import { useState, useRef, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import AuthPage        from "./pages/AuthPage";
import RiskPage        from "./pages/RiskPage";
import AdminDashboard  from "./pages/AdminDashboard";
import ChatPage        from "./pages/ChatPage";
import HealthHistoryPage from "./pages/HealthHistoryPage";
import LabUploadPage   from "./pages/LabUploadPage";
import FamilyPage      from "./pages/FamilyPage";
import Sidebar         from "./components/Sidebar";
import AIChatWidget    from "./components/AIChatWidget";

/**
 * Root component — owns global state and page routing.
 * SignalR connection is initialised once after login and stored in a ref.
 */
export default function App() {
  const [page,       setPage]       = useState("login");   // "login" | "risk" | "dashboard" | "chat" | "history" | "lab" | "family"
  const [user,       setUser]       = useState(null);       // { id, name, email, role, token }
  const [riskData,   setRiskData]   = useState(null);       // { score, level, assessmentId }
  const [activeChat, setActiveChat] = useState(null);       // active patient for chat
  const [sidebarTab, setSidebarTab] = useState("dashboard");
  const [managedProfiles, setManagedProfiles] = useState([]);
  const connectionRef = useRef(null);

  /** Called by AuthPage on successful login */
  const handleLogin = (userData) => {
    setUser(userData);
    // Route by role
    if (userData.role === "admin" || userData.role === "doctor") {
      setPage("dashboard");
    } else {
      setPage("risk");
    }
    initSignalR(userData.token);
  };

  /** Initialise SignalR connection once after login */
  const initSignalR = (token) => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("/hubs/chat", { accessTokenFactory: () => token })
      .withAutomaticReconnect()
      .build();
    connection.start().catch(console.error);
    connectionRef.current = connection;
  };

  const handleLogout = () => {
    connectionRef.current?.stop();
    setUser(null);
    setPage("login");
  };

  // ── Routing ──────────────────────────────────────────────────────────────
  if (!user) return <AuthPage onLogin={handleLogin} />;

  const showSidebar = ["dashboard", "chat", "history", "lab", "family"].includes(page);

  return (
    <div style={{ display: "flex", height: "100vh" }}>
      {showSidebar && (
        <Sidebar tab={sidebarTab} setTab={setSidebarTab} user={user} onLogout={handleLogout} setPage={setPage} />
      )}
      <main style={{ flex: 1, overflow: "auto" }}>
        {page === "risk"      && <RiskPage riskData={riskData} user={user} onLogout={handleLogout} onChat={() => setPage("chat")} />}
        {page === "dashboard" && <AdminDashboard user={user} onChatOpen={(p) => { setActiveChat(p); setPage("chat"); }} />}
        {page === "chat"      && <ChatPage connection={connectionRef.current} activePatient={activeChat} setActivePatient={setActiveChat} user={user} />}
        {page === "history"   && <HealthHistoryPage user={user} />}
        {page === "lab"       && <LabUploadPage user={user} onSuccess={() => setPage("risk")} />}
        {page === "family"    && <FamilyPage user={user} managedProfiles={managedProfiles} />}
      </main>
      {/* AI Chat Widget — visible on all patient pages */}
      {user?.role === "patient" && <AIChatWidget user={user} />}
    </div>
  );
}

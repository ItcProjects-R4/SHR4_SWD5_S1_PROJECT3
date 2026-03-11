import { useState, useRef } from "react";
import AuthPage from "./pages/AuthPage";
import RiskPage from "./pages/RiskPage";
import AdminDashboard from "./pages/AdminDashboard";
import ChatPage from "./pages/ChatPage";
import HealthHistoryPage from "./pages/HealthHistoryPage";
import LabUploadPage from "./pages/LabUploadPage";
import FamilyPage from "./pages/FamilyPage";

/**
 * Et'men Platform — Root component.
 * State-based router: page string controls which screen renders.
 * SignalR connection is initialised once after login and passed via ref.
 */
export default function App() {
  const [page, setPage]           = useState("login");
  const [user, setUser]           = useState(null);
  const [riskData, setRiskData]   = useState(null);
  const [sidebarTab, setSidebarTab] = useState("dashboard");
  const [activeChat, setActiveChat] = useState(null);
  const signalRRef                = useRef(null); // SignalR HubConnection

  const handleLogin = (userData) => {
    setUser(userData);
    if (userData.role === "admin" || userData.role === "doctor") {
      setPage("dashboard");
    } else {
      setPage("risk");
    }
  };

  const handleLogout = () => { setUser(null); setPage("login"); };

  if (!user) return <AuthPage onLogin={handleLogin} />;

  return (
    <div className="flex h-screen bg-gray-50">
      {/* TODO: Add Sidebar for doctor/admin roles */}
      {page === "risk"     && <RiskPage riskData={riskData} user={user} onLogout={handleLogout} />}
      {page === "dashboard"&& <AdminDashboard user={user} onLogout={handleLogout} />}
      {page === "chat"     && <ChatPage user={user} activeChat={activeChat} signalR={signalRRef} />}
      {page === "history"  && <HealthHistoryPage user={user} />}
      {page === "lab"      && <LabUploadPage user={user} />}
      {page === "family"   && <FamilyPage user={user} />}
    </div>
  );
}

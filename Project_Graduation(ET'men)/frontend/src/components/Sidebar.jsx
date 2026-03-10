/**
 * Left navigation sidebar shown for Doctor / Admin pages.
 * Props: tab, setTab, user, onLogout, setPage
 */
export default function Sidebar({ tab, setTab, user, onLogout, setPage }) {
  const isDoctor = user?.role === "doctor" || user?.role === "admin";
  const navItems = [
    { id: "dashboard", icon: "📊", label: "لوحة التحكم" },
    { id: "chat",      icon: "💬", label: "المحادثات" },
    ...(user?.role === "patient" ? [
      { id: "history", icon: "📈", label: "سجل الصحة" },
      { id: "lab",     icon: "📎", label: "رفع التحاليل" },
      { id: "family",  icon: "👨‍👩‍👧", label: "الحساب العائلي" },
    ] : []),
  ];

  return (
    <div style={{ width: 240, background: "#1e293b", display: "flex", flexDirection: "column", height: "100vh" }}>
      <div style={{ padding: "1.5rem", borderBottom: "1px solid #0f172a" }}>
        <h2 style={{ color: "#fff", margin: 0, fontSize: "1.1rem" }}>🫀 Etmen</h2>
        <p style={{ color: "#64748b", fontSize: "0.8rem", margin: "0.25rem 0 0" }}>{user?.name}</p>
      </div>
      <nav style={{ flex: 1, padding: "1rem" }}>
        {navItems.map(item => (
          <button key={item.id} onClick={() => { setTab(item.id); setPage(item.id); }}
            style={{ display: "flex", alignItems: "center", gap: "0.75rem", width: "100%", background: tab===item.id ? "#0f172a" : "transparent", border: "none", borderRadius: 8, padding: "0.75rem 1rem", cursor: "pointer", color: tab===item.id ? "#60a5fa" : "#94a3b8", marginBottom: "0.25rem", textAlign: "right" }}>
            <span>{item.icon}</span><span>{item.label}</span>
          </button>
        ))}
      </nav>
      <div style={{ padding: "1rem", borderTop: "1px solid #0f172a" }}>
        <button onClick={onLogout} style={{ width: "100%", background: "#ef44441a", border: "1px solid #ef4444", borderRadius: 8, padding: "0.6rem", cursor: "pointer", color: "#ef4444" }}>تسجيل الخروج</button>
      </div>
    </div>
  );
}

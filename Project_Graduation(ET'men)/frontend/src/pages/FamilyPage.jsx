import { useState, useEffect } from "react";

/**
 * Family account linking management page (v3.0).
 * Primary account holder can view, invite, and manage linked family profiles.
 * Props: user, managedProfiles
 */
export default function FamilyPage({ user, managedProfiles }) {
  const [members, setMembers] = useState([]);
  const [email,   setEmail]   = useState("");
  const [rel,     setRel]     = useState("Child");
  const [loading, setLoading] = useState(false);
  const [msg,     setMsg]     = useState("");

  useEffect(() => {
    fetch("/api/family", { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } })
      .then(r => r.json()).then(setMembers).catch(() => {});
  }, []);

  const invite = async () => {
    setLoading(true); setMsg("");
    try {
      await fetch("/api/family/invite", {
        method: "POST",
        headers: { "Content-Type": "application/json", Authorization: `Bearer ${localStorage.getItem("token")}` },
        body: JSON.stringify({ primaryUserId: user.id, email, relationship: rel }),
      });
      setMsg("✅ تم إرسال الدعوة إلى " + email);
      setEmail("");
    } catch { setMsg("❌ فشل الإرسال"); }
    finally   { setLoading(false); }
  };

  return (
    <div style={{ padding: "2rem", background: "#0f172a", minHeight: "100vh", color: "#fff" }}>
      <h1 style={{ marginBottom: "2rem" }}>👨‍👩‍👧 إدارة الحساب العائلي</h1>

      {/* Invite Form */}
      <div style={{ background: "#1e293b", borderRadius: 16, padding: "2rem", maxWidth: 500, marginBottom: "2rem" }}>
        <h3 style={{ marginBottom: "1rem" }}>إضافة فرد عائلي</h3>
        <input value={email} onChange={e => setEmail(e.target.value)} placeholder="البريد الإلكتروني"
          style={{ width: "100%", background: "#0f172a", border: "1px solid #334155", borderRadius: 8, padding: "0.7rem", color: "#fff", marginBottom: "0.75rem" }} />
        <select value={rel} onChange={e => setRel(e.target.value)}
          style={{ width: "100%", background: "#0f172a", border: "1px solid #334155", borderRadius: 8, padding: "0.7rem", color: "#fff", marginBottom: "0.75rem" }}>
          {["Child","Parent","Spouse","Sibling","Caregiver"].map(r => <option key={r}>{r}</option>)}
        </select>
        {msg && <p style={{ color: msg.startsWith("✅") ? "#22c55e" : "#ef4444", marginBottom: "0.75rem" }}>{msg}</p>}
        <button onClick={invite} disabled={loading}
          style={{ background: "#3b82f6", color: "#fff", border: "none", borderRadius: 8, padding: "0.7rem 1.5rem", cursor: "pointer", fontWeight: 600 }}>
          {loading ? "جارٍ الإرسال..." : "إرسال دعوة"}
        </button>
      </div>

      {/* Members List */}
      <h3 style={{ marginBottom: "1rem" }}>الأفراد المرتبطون</h3>
      {members.length === 0
        ? <p style={{ color: "#64748b" }}>لم يتم ربط أي حسابات بعد.</p>
        : members.map(m => (
            <div key={m.id} style={{ background: "#1e293b", borderRadius: 12, padding: "1rem", marginBottom: "0.75rem", display: "flex", justifyContent: "space-between" }}>
              <div><strong>{m.name}</strong><span style={{ color: "#64748b", marginRight: "0.5rem" }}>({m.relationship})</span></div>
              <span style={{ color: m.riskLevel==="High" ? "#ef4444" : m.riskLevel==="Medium" ? "#f59e0b" : "#22c55e" }}>{m.riskLevel}</span>
            </div>
          ))
      }
    </div>
  );
}

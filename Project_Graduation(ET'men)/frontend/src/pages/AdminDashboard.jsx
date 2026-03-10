import { useState, useEffect } from "react";

/**
 * Admin / Doctor dashboard — stats, risk chart, filterable patient table.
 * Props: user, onChatOpen(patient)
 */
export default function AdminDashboard({ user, onChatOpen }) {
  const [patients, setPatients] = useState([]);
  const [filter,   setFilter]   = useState("all");
  const [search,   setSearch]   = useState("");

  useEffect(() => {
    fetch("/api/admin/dashboard", { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } })
      .then(r => r.json())
      .then(d => setPatients(d.patients ?? []))
      .catch(() => setPatients(MOCK_PATIENTS));
  }, []);

  const filtered = patients.filter(p =>
    (filter === "all" || p.level === filter) &&
    (p.name.includes(search) || p.doctor?.includes(search))
  );

  return (
    <div style={{ padding: "2rem", background: "#0f172a", minHeight: "100vh", color: "#fff" }}>
      <h1 style={{ marginBottom: "0.25rem" }}>لوحة تحكم المشرف</h1>
      <p style={{ color: "#64748b", marginBottom: "2rem" }}>آخر تحديث: الآن</p>

      {/* Stats */}
      <div style={{ display: "grid", gridTemplateColumns: "repeat(4,1fr)", gap: "1rem", marginBottom: "2rem" }}>
        {[{ l: "إجمالي المرضى", v: patients.length, c: "#3b82f6" }, { l: "خطر مرتفع", v: patients.filter(p=>p.level==="high").length, c: "#ef4444" }, { l: "خطر متوسط", v: patients.filter(p=>p.level==="medium").length, c: "#f59e0b" }, { l: "خطر منخفض", v: patients.filter(p=>p.level==="low").length, c: "#22c55e" }].map(s => (
          <div key={s.l} style={{ background: "#1e293b", borderRadius: 12, padding: "1.5rem", borderTop: `3px solid ${s.c}` }}>
            <div style={{ fontSize: "2rem", fontWeight: 700, color: s.c }}>{s.v}</div>
            <div style={{ color: "#94a3b8", marginTop: "0.25rem" }}>{s.l}</div>
          </div>
        ))}
      </div>

      {/* Filter bar */}
      <div style={{ display: "flex", gap: "1rem", marginBottom: "1.5rem" }}>
        <input value={search} onChange={e => setSearch(e.target.value)} placeholder="بحث..." style={{ flex: 1, background: "#1e293b", border: "1px solid #334155", borderRadius: 8, padding: "0.6rem 1rem", color: "#fff" }} />
        {["all","high","medium","low"].map(f => (
          <button key={f} onClick={() => setFilter(f)}
            style={{ padding: "0.6rem 1.2rem", borderRadius: 8, border: "none", cursor: "pointer", background: filter===f ? "#3b82f6" : "#1e293b", color: "#fff" }}>
            {f==="all"?"الكل":f==="high"?"عالي":f==="medium"?"متوسط":"منخفض"}
          </button>
        ))}
      </div>

      {/* Table */}
      <div style={{ background: "#1e293b", borderRadius: 12, overflow: "hidden" }}>
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr style={{ background: "#0f172a" }}>
              {["المريض","العمر","مؤشر الخطر","الضغط","السكر","الطبيب","إجراء"].map(h => (
                <th key={h} style={{ padding: "1rem", textAlign: "right", color: "#94a3b8", fontWeight: 600 }}>{h}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {filtered.map(p => (
              <tr key={p.id} style={{ borderTop: "1px solid #0f172a" }}>
                <td style={{ padding: "1rem" }}><strong>{p.name}</strong><br/><span style={{ color: "#64748b", fontSize: "0.8rem" }}>{p.issue}</span></td>
                <td style={{ padding: "1rem" }}>{p.age}</td>
                <td style={{ padding: "1rem" }}><div style={{ background: "#0f172a", borderRadius: 9999, height: 8, width: 100 }}><div style={{ background: p.level==="high"?"#ef4444":p.level==="medium"?"#f59e0b":"#22c55e", width: `${p.risk*100}%`, height: "100%", borderRadius: 9999 }}/></div>{Math.round(p.risk*100)}%</td>
                <td style={{ padding: "1rem", color: p.level==="high"?"#ef4444":"inherit" }}>{p.bp}</td>
                <td style={{ padding: "1rem", color: p.level==="high"?"#ef4444":"inherit" }}>{p.sugar}</td>
                <td style={{ padding: "1rem" }}>{p.doctor}</td>
                <td style={{ padding: "1rem" }}><button onClick={() => onChatOpen(p)} style={{ background: "#3b82f6", color: "#fff", border: "none", borderRadius: 8, padding: "0.4rem 0.8rem", cursor: "pointer" }}>مراسلة</button></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

const MOCK_PATIENTS = [
  { id: "1", name: "أحمد محمد", age: 45, risk: 0.82, level: "high",   bp: "160/95", sugar: "210", doctor: "د. سارة", issue: "ضغط + سكر" },
  { id: "2", name: "فاطمة علي",  age: 38, risk: 0.45, level: "medium", bp: "130/85", sugar: "108", doctor: "د. خالد", issue: "وزن زائد" },
  { id: "3", name: "محمد حسن",  age: 55, risk: 0.21, level: "low",    bp: "120/80", sugar: "95",  doctor: "د. نورا", issue: "—" },
];

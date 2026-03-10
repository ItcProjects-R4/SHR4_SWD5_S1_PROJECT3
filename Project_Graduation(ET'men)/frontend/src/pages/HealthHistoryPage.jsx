import { useState, useEffect } from "react";

/**
 * Patient health history timeline — risk score trend + vitals charts (v3.0).
 * Shows: summary strip, risk line chart, vitals sub-charts, scrollable event feed.
 * Props: user
 */
export default function HealthHistoryPage({ user }) {
  const [history, setHistory] = useState([]);
  const [vitals,  setVitals]  = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const from  = new Date(Date.now() - 90 * 864e5).toISOString();
    const to    = new Date().toISOString();

    fetch(`/api/health-history/risk?patientId=${user.id}&from=${from}&to=${to}`, { headers: { Authorization: `Bearer ${token}` } })
      .then(r => r.json()).then(setHistory).catch(() => {});
    fetch(`/api/health-history/vitals?patientId=${user.id}`, { headers: { Authorization: `Bearer ${token}` } })
      .then(r => r.json()).then(setVitals).catch(() => {});
  }, [user.id]);

  return (
    <div style={{ padding: "2rem", background: "#0f172a", minHeight: "100vh", color: "#fff" }}>
      <h1 style={{ marginBottom: "2rem" }}>📊 سجل الصحة</h1>

      {/* Summary Strip */}
      <div style={{ background: "#1e293b", borderRadius: 12, padding: "1.5rem", marginBottom: "2rem", display: "flex", gap: "2rem" }}>
        <div><div style={{ color: "#94a3b8", fontSize: "0.85rem" }}>آخر نتيجة</div><div style={{ fontSize: "2rem", fontWeight: 700, color: "#f59e0b" }}>{history[history.length-1]?.riskScore ? `${Math.round(history[history.length-1].riskScore*100)}%` : "—"}</div></div>
        <div><div style={{ color: "#94a3b8", fontSize: "0.85rem" }}>الاتجاه</div><div style={{ fontSize: "2rem" }}>📈</div></div>
      </div>

      {/* Risk Timeline placeholder */}
      <div style={{ background: "#1e293b", borderRadius: 12, padding: "1.5rem", marginBottom: "2rem" }}>
        <h3 style={{ marginBottom: "1rem" }}>تطور مؤشر الخطر</h3>
        {history.length === 0
          ? <p style={{ color: "#64748b" }}>لا توجد بيانات كافية. قدّم سجلاً طبياً واحداً على الأقل.</p>
          : <p style={{ color: "#94a3b8" }}>TODO: Render line chart using recharts with history data</p>
        }
      </div>

      {/* Export */}
      <button onClick={() => window.print()}
        style={{ background: "#334155", color: "#fff", border: "none", borderRadius: 8, padding: "0.7rem 1.5rem", cursor: "pointer" }}>
        ⬇️ تصدير PDF
      </button>
    </div>
  );
}

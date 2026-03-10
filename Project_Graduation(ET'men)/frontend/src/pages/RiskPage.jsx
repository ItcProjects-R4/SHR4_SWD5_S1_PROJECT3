import { useState, useEffect } from "react";
import NearbyFinder from "../components/NearbyFinder";
import RiskGauge    from "../components/RiskGauge";
import { useGeolocation } from "../hooks/useGeolocation";

/**
 * Patient risk result page — shown after login or after submitting health data.
 * Displays: SVG gauge, vitals, required analyses, doctor booking, and NearbyFinder.
 * Props: riskData { score, level, assessmentId }, user, onLogout, onChat
 */
export default function RiskPage({ riskData, user, onLogout, onChat }) {
  const [booked,    setBooked]    = useState(false);
  const [providers, setProviders] = useState([]);
  const { location, requestLocation } = useGeolocation();

  const risk    = riskData ?? { score: 0.45, level: "medium", assessmentId: null };
  const pct     = Math.round(risk.score * 100);
  const details = RISK_DETAILS[risk.level] ?? RISK_DETAILS.medium;

  // Auto-request GPS for high-risk patients
  useEffect(() => {
    if (risk.level === "high") {
      const t = setTimeout(requestLocation, 2000);
      return () => clearTimeout(t);
    }
  }, [risk.level]);

  // Fetch providers when GPS is available
  useEffect(() => {
    if (!location || !risk.assessmentId) return;
    fetch(`/api/nearby/providers?lat=${location.lat}&lng=${location.lng}&riskLevel=${risk.level}`,
      { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } })
      .then(r => r.json()).then(setProviders).catch(console.error);
  }, [location]);

  return (
    <div style={{ minHeight: "100vh", background: "#0f172a", color: "#fff", padding: "2rem" }}>
      {/* Header */}
      <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "2rem" }}>
        <h1>🫀 نتيجة تقييم المخاطر</h1>
        <div style={{ display: "flex", gap: "1rem" }}>
          <button onClick={onChat} style={btnStyle("#3b82f6")}>💬 تواصل مع الطبيب</button>
          <button onClick={onLogout} style={btnStyle("#ef4444")}>تسجيل الخروج</button>
        </div>
      </div>

      {/* Main Risk Card */}
      <div style={{ background: "#1e293b", borderRadius: 16, padding: "2rem", display: "flex", gap: "2rem", marginBottom: "2rem" }}>
        <RiskGauge percentage={pct} color={details.color} />
        <div>
          <div style={{ background: details.bg, border: `1px solid ${details.border}`, borderRadius: 8, padding: "0.5rem 1rem", display: "inline-block", marginBottom: "1rem", color: details.color, fontWeight: 700 }}>
            {details.emoji} {details.label}
          </div>
          <p style={{ color: "#cbd5e1", marginBottom: "1.5rem" }}>{details.message}</p>
          <div style={{ display: "flex", gap: "1rem" }}>
            {[{ l: "الضغط", v: "130/85" }, { l: "السكر", v: "108 mg/dL" }, { l: "BMI", v: "27.3" }].map(v => (
              <div key={v.l} style={{ background: "#0f172a", borderRadius: 10, padding: "0.75rem 1.25rem", textAlign: "center" }}>
                <div style={{ color: "#94a3b8", fontSize: "0.8rem" }}>{v.l}</div>
                <div style={{ fontWeight: 700 }}>{v.v}</div>
              </div>
            ))}
          </div>
        </div>
      </div>

      {/* Required Analyses */}
      {details.analyses.length > 0 && (
        <section style={{ marginBottom: "2rem" }}>
          <h2 style={{ marginBottom: "1rem" }}>🧪 التحاليل المطلوبة</h2>
          <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "1rem" }}>
            {details.analyses.map(a => (
              <div key={a.name} style={{ background: "#1e293b", borderRadius: 12, padding: "1rem" }}>
                <span style={{ fontSize: "1.5rem" }}>{a.icon}</span>
                <h4 style={{ margin: "0.5rem 0 0.25rem" }}>{a.name}</h4>
                <span style={{ fontSize: "0.75rem", background: a.priority === "Urgent" ? "#ef44441a" : "#3b82f61a", color: a.priority === "Urgent" ? "#ef4444" : "#3b82f6", padding: "0.2rem 0.6rem", borderRadius: 9999 }}>{a.priority}</span>
                <p style={{ color: "#94a3b8", fontSize: "0.85rem", marginTop: "0.5rem" }}>{a.desc}</p>
              </div>
            ))}
          </div>
        </section>
      )}

      {/* Nearby Finder */}
      {location && <NearbyFinder providers={providers} onBook={() => setBooked(true)} />}
      {booked    && <div style={{ background: "#22c55e1a", border: "1px solid #22c55e", borderRadius: 12, padding: "1rem", marginTop: "1rem", color: "#22c55e" }}>✅ تم حجز الموعد بنجاح!</div>}
    </div>
  );
}

const btnStyle = (bg) => ({
  background: bg, color: "#fff", border: "none", borderRadius: 8,
  padding: "0.6rem 1rem", cursor: "pointer", fontWeight: 600
});

// Static config — replace analyses/doctors with real API calls in production
const RISK_DETAILS = {
  high:   { color: "#ef4444", bg: "#ef44441a", border: "#ef4444", label: "خطر مرتفع", emoji: "🚨", message: "يستلزم تدخلاً طبياً فورياً. يُرجى التواصل مع طبيبك.", analyses: [{ icon: "🩸", name: "HbA1c", desc: "قياس السكر التراكمي", priority: "Urgent" }, { icon: "❤️", name: "تخطيط القلب", desc: "ECG", priority: "Urgent" }], doctors: [] },
  medium: { color: "#f59e0b", bg: "#f59e0b1a", border: "#f59e0b", label: "خطر متوسط", emoji: "⚠️", message: "يُنصح بمراجعة الطبيب خلال أسبوع.", analyses: [{ icon: "🔬", name: "دهون الدم", desc: "Lipid Profile", priority: "WithinWeek" }], doctors: [] },
  low:    { color: "#22c55e", bg: "#22c55e1a", border: "#22c55e", label: "خطر منخفض", emoji: "✅", message: "وضعك الصحي جيد. استمر في نمط الحياة الصحي.", analyses: [], doctors: [] },
};

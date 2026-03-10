/**
 * GPS-based healthcare provider cards (v3.0).
 * Displayed below the analyses section on RiskPage when location is available.
 * Props: providers (NearbyProvider[]), onBook(provider)
 */
export default function NearbyFinder({ providers, onBook }) {
  if (!providers?.length) return (
    <div style={{ background: "#1e293b", borderRadius: 12, padding: "1.5rem", color: "#64748b" }}>
      🔍 جارٍ البحث عن أقرب الأطباء...
    </div>
  );

  return (
    <section>
      <h2 style={{ color: "#fff", marginBottom: "1rem" }}>📍 أقرب الأطباء والمستشفيات</h2>
      <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fill,minmax(280px,1fr))", gap: "1rem" }}>
        {providers.map(p => (
          <div key={p.id} style={{ background: "#1e293b", borderRadius: 12, padding: "1.25rem", border: p.isRegistered ? "1px solid #3b82f6" : "1px solid #334155" }}>
            {p.isRegistered && <span style={{ fontSize: "0.7rem", background: "#3b82f61a", color: "#60a5fa", padding: "0.2rem 0.6rem", borderRadius: 9999, marginBottom: "0.5rem", display: "inline-block" }}>✔ مسجّل</span>}
            <h4 style={{ color: "#fff", margin: "0.5rem 0 0.25rem" }}>{p.name}</h4>
            <p style={{ color: "#94a3b8", fontSize: "0.85rem", margin: "0 0 0.5rem" }}>{p.specialty}</p>
            <div style={{ display: "flex", justifyContent: "space-between", color: "#64748b", fontSize: "0.8rem", marginBottom: "1rem" }}>
              <span>⭐ {p.rating?.toFixed(1)}</span>
              <span>📏 {p.distanceMeters < 1000 ? `${Math.round(p.distanceMeters)} م` : `${(p.distanceMeters/1000).toFixed(1)} كم`}</span>
            </div>
            <button onClick={() => onBook(p)}
              style={{ width: "100%", background: "#3b82f6", color: "#fff", border: "none", borderRadius: 8, padding: "0.6rem", cursor: "pointer", fontWeight: 600 }}>
              احجز موعداً
            </button>
          </div>
        ))}
      </div>
    </section>
  );
}

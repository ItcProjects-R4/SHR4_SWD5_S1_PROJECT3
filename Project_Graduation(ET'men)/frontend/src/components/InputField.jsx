/**
 * Styled form input with optional leading icon and password visibility toggle.
 * Props: icon, placeholder, value, onChange, type, showToggle, onToggle
 */
export default function InputField({ icon, placeholder, value, onChange, type = "text", showToggle, onToggle }) {
  return (
    <div style={{ position: "relative", marginBottom: "1rem" }}>
      {icon && <span style={{ position: "absolute", left: "0.75rem", top: "50%", transform: "translateY(-50%)", color: "#64748b" }}>{icon}</span>}
      <input type={type} value={value} onChange={onChange} placeholder={placeholder}
        style={{ width: "100%", background: "#1e293b", border: "1px solid #334155", borderRadius: 10, padding: `0.8rem ${showToggle ? "2.5rem" : "1rem"} 0.8rem ${icon ? "2.5rem" : "1rem"}`, color: "#fff", fontSize: "0.95rem", outline: "none", boxSizing: "border-box" }} />
      {showToggle && (
        <button type="button" onClick={onToggle}
          style={{ position: "absolute", right: "0.75rem", top: "50%", transform: "translateY(-50%)", background: "none", border: "none", cursor: "pointer", color: "#64748b" }}>
          {type === "password" ? "👁" : "🙈"}
        </button>
      )}
    </div>
  );
}

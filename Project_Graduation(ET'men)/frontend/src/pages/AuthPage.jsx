import { useState } from "react";
import InputField from "../components/InputField";

/**
 * Login + Signup page with Google OAuth button and animated background.
 * Supports role selection (Patient / Doctor / Admin) on signup.
 * Props: onLogin(userData)
 */
export default function AuthPage({ onLogin }) {
  const [mode,          setMode]          = useState("login");   // "login" | "signup"
  const [email,         setEmail]         = useState("");
  const [password,      setPassword]      = useState("");
  const [name,          setName]          = useState("");
  const [role,          setRole]          = useState("patient");
  const [showPass,      setShowPass]      = useState(false);
  const [loading,       setLoading]       = useState(false);
  const [googleLoading, setGoogleLoading] = useState(false);
  const [error,         setError]         = useState("");
  const [success,       setSuccess]       = useState("");

  const handleSubmit = async () => {
    setLoading(true); setError("");
    try {
      const endpoint = mode === "login" ? "/api/auth/login" : "/api/auth/register";
      const body     = mode === "login"
        ? { email, password }
        : { fullName: name, email, password, role, dateOfBirth: "1990-01-01", gender: "Male" };

      const res  = await fetch(endpoint, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });
      if (!res.ok) { setError("بيانات غير صحيحة"); return; }
      const data = await res.json();
      localStorage.setItem("token", data.accessToken);
      onLogin({ id: data.userId, name: data.fullName, email, role: data.role.toLowerCase(), token: data.accessToken });
    } catch { setError("خطأ في الاتصال"); }
    finally  { setLoading(false); }
  };

  const handleGoogle = async () => {
    setGoogleLoading(true);
    // TODO: integrate Google OAuth SDK to get idToken, then call /api/auth/google/callback
    setGoogleLoading(false);
  };

  return (
    <div style={{ display: "flex", minHeight: "100vh", background: "#0f172a", color: "#fff" }}>
      {/* Left Panel */}
      <div style={{ flex: 1, display: "flex", flexDirection: "column", justifyContent: "center", padding: "3rem" }}>
        <h1 style={{ fontSize: "1.8rem", marginBottom: "0.5rem" }}>🫀 اطمِن — Et'men</h1>
        <p style={{ color: "#94a3b8", marginBottom: "2rem" }}>منصة الرعاية الصحية الذكية</p>

        {error   && <div style={{ background: "#ef44441a", border: "1px solid #ef4444", borderRadius: 8, padding: "0.75rem", marginBottom: "1rem" }}>{error}</div>}
        {success && <div style={{ background: "#22c55e1a", border: "1px solid #22c55e", borderRadius: 8, padding: "0.75rem", marginBottom: "1rem" }}>{success}</div>}

        <button onClick={handleGoogle} disabled={googleLoading} style={{ background: "#fff", color: "#1e293b", border: "none", borderRadius: 10, padding: "0.8rem", cursor: "pointer", marginBottom: "1.5rem", fontWeight: 600 }}>
          {googleLoading ? "جارٍ التحميل..." : "🔵 تسجيل الدخول بـ Google"}
        </button>

        {mode === "signup" && (
          <InputField placeholder="الاسم الكامل" value={name} onChange={e => setName(e.target.value)} />
        )}
        <InputField placeholder="البريد الإلكتروني" value={email} onChange={e => setEmail(e.target.value)} type="email" />
        <InputField placeholder="كلمة المرور" value={password} onChange={e => setPassword(e.target.value)}
          type={showPass ? "text" : "password"} showToggle onToggle={() => setShowPass(!showPass)} />

        {mode === "signup" && (
          <div style={{ display: "flex", gap: "0.5rem", marginBottom: "1rem" }}>
            {["patient","doctor","admin"].map(r => (
              <button key={r} onClick={() => setRole(r)}
                style={{ flex: 1, padding: "0.6rem", borderRadius: 8, border: "none", cursor: "pointer",
                  background: role === r ? "#3b82f6" : "#1e293b", color: "#fff" }}>
                {r === "patient" ? "مريض" : r === "doctor" ? "طبيب" : "مشرف"}
              </button>
            ))}
          </div>
        )}

        <button onClick={handleSubmit} disabled={loading}
          style={{ background: "linear-gradient(135deg,#3b82f6,#8b5cf6)", color: "#fff", border: "none", borderRadius: 10, padding: "0.9rem", cursor: "pointer", fontWeight: 700, fontSize: "1rem" }}>
          {loading ? "جارٍ التحميل..." : mode === "login" ? "تسجيل الدخول" : "إنشاء حساب"}
        </button>

        <p style={{ marginTop: "1rem", color: "#94a3b8", textAlign: "center" }}>
          {mode === "login" ? "ليس لديك حساب؟" : "لديك حساب بالفعل؟"}
          <span onClick={() => setMode(mode === "login" ? "signup" : "login")}
            style={{ color: "#3b82f6", cursor: "pointer", marginRight: "0.4rem" }}>
            {mode === "login" ? "إنشاء حساب" : "تسجيل الدخول"}
          </span>
        </p>
      </div>

      {/* Right Panel */}
      <div style={{ width: "42%", background: "linear-gradient(135deg,#1e3a5f,#312e81)", display: "flex", flexDirection: "column", justifyContent: "center", padding: "3rem" }}>
        <span style={{ color: "#60a5fa", fontWeight: 600, marginBottom: "1rem" }}>AI-Powered Healthcare</span>
        <h2 style={{ fontSize: "2rem", marginBottom: "1rem" }}>التدخل المبكر ينقذ الأرواح</h2>
        <p style={{ color: "#cbd5e1", marginBottom: "2rem" }}>منصة تحليل البيانات الصحية بالذكاء الاصطناعي لتقييم مخاطر الأمراض المزمنة</p>
        {[{ n: "98.4%", l: "دقة التنبؤ" }, { n: "+12,000", l: "مريض" }, { n: "+340", l: "حالة منقذة" }].map(s => (
          <div key={s.l} style={{ display: "flex", justifyContent: "space-between", padding: "0.75rem 0", borderBottom: "1px solid #ffffff1a" }}>
            <span style={{ color: "#e2e8f0" }}>{s.l}</span>
            <span style={{ color: "#60a5fa", fontWeight: 700 }}>{s.n}</span>
          </div>
        ))}
      </div>
    </div>
  );
}

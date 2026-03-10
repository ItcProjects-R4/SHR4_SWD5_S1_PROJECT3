import { useState } from "react";

/**
 * Lab result OCR upload page (v3.0).
 * Patient uploads a PDF or image → backend runs OCR → risk score re-calculated.
 * Props: user, onSuccess() — called when upload succeeds
 */
export default function LabUploadPage({ user, onSuccess }) {
  const [file,     setFile]     = useState(null);
  const [loading,  setLoading]  = useState(false);
  const [error,    setError]    = useState("");
  const [result,   setResult]   = useState(null);

  const handleUpload = async () => {
    if (!file) { setError("يرجى اختيار ملف"); return; }
    setLoading(true); setError("");
    try {
      const form = new FormData();
      form.append("file",      file);
      form.append("patientId", user.id);

      const res = await fetch("/api/lab-results/upload", {
        method: "POST",
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
        body: form,
      });
      if (!res.ok) throw new Error("فشل الرفع");
      const data = await res.json();
      setResult(data);
      setTimeout(onSuccess, 2000);
    } catch (e) { setError(e.message); }
    finally     { setLoading(false); }
  };

  return (
    <div style={{ padding: "2rem", background: "#0f172a", minHeight: "100vh", color: "#fff" }}>
      <h1 style={{ marginBottom: "2rem" }}>📎 رفع نتائج التحاليل</h1>

      <div style={{ background: "#1e293b", borderRadius: 16, padding: "2rem", maxWidth: 500 }}>
        <p style={{ color: "#94a3b8", marginBottom: "1.5rem" }}>
          ارفع صورة أو PDF لنتيجة تحليلك. سيقوم النظام باستخراج القيم تلقائياً وتحديث مؤشر الخطر.
        </p>

        <input type="file" accept=".pdf,image/*" onChange={e => setFile(e.target.files[0])}
          style={{ display: "block", marginBottom: "1rem", color: "#94a3b8" }} />

        {file && <p style={{ color: "#60a5fa", marginBottom: "1rem" }}>✔ {file.name}</p>}
        {error && <p style={{ color: "#ef4444", marginBottom: "1rem" }}>{error}</p>}

        <button onClick={handleUpload} disabled={loading}
          style={{ background: "linear-gradient(135deg,#3b82f6,#8b5cf6)", color: "#fff", border: "none", borderRadius: 10, padding: "0.9rem 2rem", cursor: "pointer", fontWeight: 700 }}>
          {loading ? "⏳ جارٍ التحليل..." : "رفع وتحليل"}
        </button>

        {result && (
          <div style={{ marginTop: "1.5rem", background: "#22c55e1a", border: "1px solid #22c55e", borderRadius: 10, padding: "1rem", color: "#22c55e" }}>
            ✅ تم استخراج القيم وتحديث مؤشر الخطر إلى {Math.round(result.score * 100)}%
          </div>
        )}
      </div>
    </div>
  );
}

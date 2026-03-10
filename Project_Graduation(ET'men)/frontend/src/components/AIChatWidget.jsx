import { useState, useRef, useEffect } from "react";

/**
 * Floating AI Chat Assistant widget (v3.0) — bottom-right of all patient pages.
 * Completely separate from the Doctor–Patient SignalR chat.
 * Props: user { id, name, token }
 */
export default function AIChatWidget({ user }) {
  const [isOpen,         setIsOpen]         = useState(false);
  const [messages,       setMessages]       = useState([]);
  const [input,          setInput]          = useState("");
  const [isTyping,       setIsTyping]       = useState(false);
  const [showDoctorCTA,  setShowDoctorCTA]  = useState(false);
  const [showCrisisBanner, setShowCrisisBanner] = useState(false);
  const endRef = useRef(null);

  useEffect(() => { endRef.current?.scrollIntoView({ behavior: "smooth" }); }, [messages, isTyping]);

  const send = async () => {
    if (!input.trim()) return;
    const userMsg = { role: "user", text: input, timestamp: new Date().toLocaleTimeString("ar-EG") };
    setMessages(prev => [...prev, userMsg]);
    setInput(""); setIsTyping(true); setShowDoctorCTA(false); setShowCrisisBanner(false);

    try {
      const history = { turns: messages.slice(-10).map(m => ({ role: m.role, text: m.text })) };
      const res  = await fetch("/api/ai-chat/ask", {
        method: "POST",
        headers: { "Content-Type": "application/json", Authorization: `Bearer ${localStorage.getItem("token")}` },
        body: JSON.stringify({ patientId: user.id, message: userMsg.text, history }),
      });
      const data = await res.json();
      setMessages(prev => [...prev, { role: "assistant", text: data.reply, timestamp: new Date().toLocaleTimeString("ar-EG") }]);
      setShowDoctorCTA(data.suggestDoctorChat);
      setShowCrisisBanner(data.detectedCrisis);
    } catch {
      setMessages(prev => [...prev, { role: "assistant", text: "حدث خطأ. يرجى المحاولة مجدداً.", timestamp: "" }]);
    } finally { setIsTyping(false); }
  };

  return (
    <>
      {/* Floating Button */}
      <button onClick={() => setIsOpen(!isOpen)}
        style={{ position: "fixed", bottom: "2rem", left: "2rem", width: 56, height: 56, borderRadius: "50%", background: "linear-gradient(135deg,#3b82f6,#8b5cf6)", border: "none", cursor: "pointer", fontSize: "1.5rem", boxShadow: "0 4px 20px #3b82f640", zIndex: 1000 }}>
        {isOpen ? "✕" : "🤖"}
      </button>

      {/* Chat Window */}
      {isOpen && (
        <div style={{ position: "fixed", bottom: "5rem", left: "2rem", width: 340, background: "#1e293b", borderRadius: 16, boxShadow: "0 8px 32px #00000060", zIndex: 1000, display: "flex", flexDirection: "column", maxHeight: 480 }}>
          {/* Header */}
          <div style={{ padding: "1rem", borderBottom: "1px solid #0f172a" }}>
            <strong style={{ color: "#fff" }}>🤖 مساعد صحي ذكي</strong>
            <p style={{ color: "#64748b", fontSize: "0.75rem", margin: "0.25rem 0 0" }}>اسأل عن تحاليلك ونتائجك</p>
          </div>

          {/* Messages */}
          <div style={{ flex: 1, overflowY: "auto", padding: "1rem", display: "flex", flexDirection: "column", gap: "0.75rem" }}>
            {messages.map((m, i) => (
              <div key={i} style={{ display: "flex", justifyContent: m.role === "user" ? "flex-end" : "flex-start" }}>
                <div style={{ background: m.role === "user" ? "#3b82f6" : "#0f172a", borderRadius: 10, padding: "0.6rem 0.9rem", maxWidth: "85%", color: "#fff", fontSize: "0.9rem" }}>
                  {m.text}
                </div>
              </div>
            ))}
            {isTyping && <div style={{ color: "#64748b", fontSize: "0.85rem" }}>يكتب المساعد...</div>}

            {showCrisisBanner && (
              <div style={{ background: "#ef44441a", border: "1px solid #ef4444", borderRadius: 8, padding: "0.75rem", color: "#ef4444", fontSize: "0.85rem" }}>
                🚨 يبدو أنك تمر بوقت صعب. تواصل مع طبيبك مباشرة الآن.
              </div>
            )}
            {showDoctorCTA && !showCrisisBanner && (
              <div style={{ background: "#3b82f61a", border: "1px solid #3b82f6", borderRadius: 8, padding: "0.75rem", color: "#60a5fa", fontSize: "0.85rem" }}>
                💬 هذا السؤال يحتاج إجابة من طبيبك في المحادثة الداخلية.
              </div>
            )}
            <div ref={endRef}/>
          </div>

          {/* Input */}
          <div style={{ padding: "0.75rem", borderTop: "1px solid #0f172a", display: "flex", gap: "0.5rem" }}>
            <input value={input} onChange={e => setInput(e.target.value)}
              onKeyDown={e => e.key === "Enter" && send()}
              placeholder="اسأل سؤالاً..."
              style={{ flex: 1, background: "#0f172a", border: "none", borderRadius: 8, padding: "0.6rem", color: "#fff", fontSize: "0.9rem" }} />
            <button onClick={send} style={{ background: "#3b82f6", color: "#fff", border: "none", borderRadius: 8, padding: "0 0.9rem", cursor: "pointer" }}>→</button>
          </div>
        </div>
      )}
    </>
  );
}

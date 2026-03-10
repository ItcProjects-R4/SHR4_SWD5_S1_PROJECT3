import { useState, useRef, useEffect } from "react";

/**
 * Doctor–Patient real-time chat using SignalR.
 * Props: connection (SignalR HubConnection), activePatient, setActivePatient, user
 */
export default function ChatPage({ connection, activePatient, setActivePatient, user }) {
  const [messages,   setMessages]   = useState([]);
  const [input,      setInput]      = useState("");
  const [typing,     setTyping]     = useState(false);
  const [search,     setSearch]     = useState("");
  const [patients,   setPatients]   = useState(MOCK_PATIENTS);
  const messagesEndRef = useRef(null);

  // Register SignalR listener
  useEffect(() => {
    if (!connection) return;
    connection.on("ReceiveMessage", (msg) => {
      setMessages(prev => [...prev, { from: "patient", text: msg.text, time: new Date(msg.sentAt).toLocaleTimeString("ar-EG") }]);
    });
    return () => connection.off("ReceiveMessage");
  }, [connection]);

  // Auto-scroll on new message
  useEffect(() => { messagesEndRef.current?.scrollIntoView({ behavior: "smooth" }); }, [messages]);

  const sendMessage = async () => {
    if (!input.trim() || !activePatient) return;
    const msg = { from: "doctor", text: input, time: new Date().toLocaleTimeString("ar-EG", { hour: "2-digit", minute: "2-digit" }) };
    setMessages(prev => [...prev, msg]);
    setInput("");

    if (connection?.state === "Connected") {
      await connection.invoke("SendMessage", {
        senderId: user.id, recipientId: activePatient.userId,
        patientId: activePatient.id, text: msg.text, senderRole: "Doctor"
      });
    } else {
      // Fallback simulation
      setTyping(true);
      setTimeout(() => { setTyping(false); setMessages(prev => [...prev, { from: "patient", text: "شكراً دكتور...", time: "" }]); }, 2000);
    }
  };

  const filtered = patients.filter(p => p.name.includes(search));

  return (
    <div style={{ display: "flex", height: "100vh", background: "#0f172a", color: "#fff" }}>
      {/* Patient List */}
      <div style={{ width: 280, background: "#1e293b", display: "flex", flexDirection: "column" }}>
        <div style={{ padding: "1rem" }}>
          <input value={search} onChange={e => setSearch(e.target.value)} placeholder="بحث في المرضى..." style={{ width: "100%", background: "#0f172a", border: "none", borderRadius: 8, padding: "0.6rem", color: "#fff" }} />
        </div>
        {filtered.map(p => (
          <div key={p.id} onClick={() => { setActivePatient(p); setMessages([]); }}
            style={{ padding: "1rem", cursor: "pointer", background: activePatient?.id===p.id ? "#0f172a" : "transparent", borderBottom: "1px solid #0f172a" }}>
            <div style={{ display: "flex", justifyContent: "space-between" }}>
              <strong>{p.name}</strong>
              {p.level==="high" && <span style={{ width: 8, height: 8, background: "#ef4444", borderRadius: "50%", display: "inline-block" }}/>}
            </div>
            <div style={{ color: "#64748b", fontSize: "0.8rem", marginTop: "0.25rem" }}>{p.lastMsg}</div>
          </div>
        ))}
      </div>

      {/* Chat Window */}
      <div style={{ flex: 1, display: "flex", flexDirection: "column" }}>
        {!activePatient ? (
          <div style={{ flex: 1, display: "flex", alignItems: "center", justifyContent: "center", color: "#64748b" }}>اختر مريضاً للبدء</div>
        ) : (
          <>
            {/* Chat Header */}
            <div style={{ padding: "1rem 1.5rem", background: "#1e293b", borderBottom: "1px solid #0f172a" }}>
              <strong>{activePatient.name}</strong>
              <span style={{ color: "#64748b", marginRight: "0.5rem", fontSize: "0.85rem" }}>— {activePatient.issue}</span>
            </div>

            {/* Messages */}
            <div style={{ flex: 1, overflowY: "auto", padding: "1.5rem", display: "flex", flexDirection: "column", gap: "0.75rem" }}>
              {messages.map((m, i) => (
                <div key={i} style={{ display: "flex", justifyContent: m.from==="doctor" ? "flex-end" : "flex-start" }}>
                  <div style={{ background: m.from==="doctor" ? "#3b82f6" : "#1e293b", borderRadius: 12, padding: "0.6rem 1rem", maxWidth: "70%" }}>
                    <p style={{ margin: 0 }}>{m.text}</p>
                    <span style={{ fontSize: "0.7rem", color: "#94a3b8" }}>{m.time}</span>
                  </div>
                </div>
              ))}
              {typing && <div style={{ color: "#94a3b8", fontSize: "0.85rem" }}>يكتب...</div>}
              <div ref={messagesEndRef}/>
            </div>

            {/* Input Bar */}
            <div style={{ padding: "1rem", background: "#1e293b", display: "flex", gap: "0.75rem" }}>
              <textarea value={input} onChange={e => setInput(e.target.value)}
                onKeyDown={e => { if (e.key==="Enter" && !e.shiftKey) { e.preventDefault(); sendMessage(); }}}
                placeholder="اكتب رسالة..." rows={1}
                style={{ flex: 1, background: "#0f172a", border: "none", borderRadius: 8, padding: "0.75rem", color: "#fff", resize: "none" }}/>
              <button onClick={sendMessage} style={{ background: "linear-gradient(135deg,#3b82f6,#8b5cf6)", color: "#fff", border: "none", borderRadius: 8, padding: "0 1.5rem", cursor: "pointer", fontWeight: 700 }}>→</button>
            </div>
          </>
        )}
      </div>
    </div>
  );
}

const MOCK_PATIENTS = [
  { id: "1", userId: "u1", name: "أحمد محمد", level: "high",   issue: "ضغط + سكر", lastMsg: "كيف أتناول الدواء؟" },
  { id: "2", userId: "u2", name: "فاطمة علي",  level: "medium", issue: "وزن زائد",  lastMsg: "شكراً دكتور" },
];

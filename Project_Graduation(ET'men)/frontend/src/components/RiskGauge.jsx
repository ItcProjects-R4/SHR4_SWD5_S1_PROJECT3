/**
 * SVG circular gauge showing the risk percentage.
 * Props: percentage (0-100), color (hex string)
 */
export default function RiskGauge({ percentage, color }) {
  const r             = 54;
  const circumference = 2 * Math.PI * r;
  const offset        = circumference - (percentage / 100) * circumference;

  return (
    <svg width={140} height={140} viewBox="0 0 120 120">
      {/* Track */}
      <circle r={r} cx={60} cy={60} fill="none" stroke="#1e293b" strokeWidth={12} />
      {/* Value arc */}
      <circle r={r} cx={60} cy={60} fill="none" stroke={color} strokeWidth={12}
        strokeLinecap="round" strokeDasharray={circumference} strokeDashoffset={offset}
        transform="rotate(-90 60 60)" style={{ transition: "stroke-dashoffset 1s ease" }} />
      {/* Label */}
      <text x={60} y={65} textAnchor="middle" fill="#fff" fontSize={20} fontWeight="700">{percentage}%</text>
    </svg>
  );
}

/**
 * mockData.js — Static mock data for development/demo.
 * Replace all usages with real API calls before production.
 */
export const MOCK_PATIENTS = [
  { id: "1", name: "أحمد محمد",    age: 45, risk: 0.87, level: "high",   bp: "150/95", sugar: 180 },
  { id: "2", name: "سارة علي",     age: 32, risk: 0.42, level: "medium", bp: "130/82", sugar: 110 },
  { id: "3", name: "محمد إبراهيم", age: 58, risk: 0.23, level: "low",    bp: "120/78", sugar:  95 },
];

export const MOCK_RISK_DATA = {
  high:   { color: "#EF4444", label: "عالي",   emoji: "🚨" },
  medium: { color: "#F59E0B", label: "متوسط",  emoji: "⚠️" },
  low:    { color: "#10B981", label: "منخفض",  emoji: "✅" },
};

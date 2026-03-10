/**
 * Development mock data — replace all usages with real API calls before production.
 */
export const MOCK_PATIENTS = [
  { id: "p1", userId: "u1", name: "أحمد محمد",  age: 45, risk: 0.82, level: "high",   bp: "160/95", sugar: "210", bmi: 31, doctor: "د. سارة", issue: "ضغط + سكر مرتفع" },
  { id: "p2", userId: "u2", name: "فاطمة علي",   age: 38, risk: 0.45, level: "medium", bp: "130/85", sugar: "108", bmi: 27, doctor: "د. خالد", issue: "وزن زائد" },
  { id: "p3", userId: "u3", name: "محمد حسن",   age: 55, risk: 0.18, level: "low",    bp: "120/80", sugar: "95",  bmi: 23, doctor: "د. نورا",  issue: "—" },
];

export const MOCK_MESSAGES = [
  { from: "doctor",  text: "مرحباً أحمد، كيف حالك اليوم؟",         time: "10:00 ص" },
  { from: "patient", text: "أشعر ببعض الدوار دكتورة",              time: "10:01 ص" },
  { from: "doctor",  text: "هل تناولت دواء الضغط الصباح؟",         time: "10:02 ص" },
];

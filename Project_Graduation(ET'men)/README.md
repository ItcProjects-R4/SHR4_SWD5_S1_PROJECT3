# اطمِن — Et'men 🫀
### *"صحتك في إيدنا"*

> منصة ذكاء اصطناعي للرعاية الصحية الاستباقية — ASP.NET Core 8 · React 18 · ML.NET · SignalR

---

## Architecture

| Layer | Technology | Purpose |
|---|---|---|
| Presentation | ASP.NET Core 8 Web API | HTTP Controllers, DTOs |
| Application | C# / MediatR (CQRS) | Use Cases, Orchestration |
| Domain | Pure C# | Entities, Value Objects, Business Rules |
| Infrastructure | EF Core 8, ML.NET, SignalR | DB, AI, Email, External APIs |
| Frontend | React 18, Tailwind CSS | Patient / Doctor / Admin UIs |

---

## Quick Start

### Backend
```bash
cd src/Etmen.API
dotnet restore
# Update appsettings.json with your DB connection string and secrets
dotnet ef database update
dotnet run
```

### Frontend
```bash
cd frontend
npm install
npm run dev
```

### AI Model Training (offline)
```bash
cd src/Etmen.Infrastructure/AI
pip install scikit-learn pandas joblib
python train_model.py --data health_data.csv --output risk_model_v1.pkl
```

---

## v3.0 Features

| # | Feature | Key Files |
|---|---|---|
| 18 | 📍 Nearby Doctor & Hospital Finder | `NearbyController`, `GoogleGeoSearchService`, `NearbyFinder.jsx` |
| 19 | 📊 Health History Dashboard | `HealthHistoryController`, `GetRiskHistoryQuery`, `HealthHistoryPage.jsx` |
| 20 | 📎 Lab Results Upload with OCR | `LabResultController`, `OcrService`, `LabUploadPage.jsx` |
| 21 | 👨‍👩‍👧 Family Account Linking | `FamilyController`, `FamilyLink`, `FamilyPage.jsx` |
| 22 | 🤖 AI Chat Assistant | `AIChatController`, `LlmPatientChatService`, `AIChatWidget.jsx` |

---

## Role Authorization Matrix

| Endpoint | Patient | Doctor | Admin |
|---|---|---|---|
| POST /medical-records | ✅ Own | ❌ | ✅ Any |
| GET /patients | ❌ | ✅ | ✅ |
| GET /admin/dashboard | ❌ | ❌ | ✅ |
| POST /risk-assessments/trigger | ❌ | ✅ | ✅ |
| POST /chat/send | ✅ | ✅ | ❌ |
| POST /lab-results/upload | ✅ | ❌ | ❌ |
| GET /api/ai-chat/ask | ✅ | ❌ | ❌ |

---

## Security Checklist
- [ ] Replace placeholder secrets in `appsettings.json` with environment variables
- [ ] Restrict `GoogleMaps.ApiKey` to server IP in Google Cloud Console
- [ ] Store `LlmChat.ApiKey` in Azure Key Vault / dotnet user-secrets
- [ ] Set `Sms.Enabled = true` once Vonage credentials are configured

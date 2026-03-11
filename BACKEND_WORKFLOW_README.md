# 🏥 Et'men Platform v3.0 — Backend Team Workflow

> **ASP.NET Core 9 · Clean Architecture · MediatR · EF Core 9 · FluentValidation · SignalR**

---

## 📋 Table of Contents

1. [👥 Team Structure & Responsibilities](#section-1)
2. [🔄 Layer Dependency Flow](#section-2)
3. [🌿 Merge Points & Branch Strategy](#section-3)
4. [⚙️ Configuration Setup](#section-4)
5. [📐 Coding Standards](#section-5)
6. [🚫 Out of Scope](#section-6)
7. [✅ Delivery Checklists](#section-7)
8. [🔐 Security Warnings](#section-8)
9. [📞 Full Endpoints Reference](#section-9)

---

<a id="section-1"></a>
## 👥 Section 1: Team Structure & Responsibilities

### 1.1 Team Overview

```
                    ┌─────────────────────────────────────┐
                    │         Et'men Backend Team         │
                    └─────────────────┬───────────────────┘
                                      │
      ┌────────────┬──────────┬───────┴──────┬────────────┐
      │            │          │              │            │
 ┌────▼────┐ ┌─────▼────┐ ┌──▼────┐ ┌───────▼──┐ ┌──────▼──┐
 │  Dev 1  │ │  Dev 2   │ │ Dev 3 │ │  Dev 4   │ │  Dev 5  │
 │  Lead   │ │ Senior   │ │Senior │ │   Mid    │ │ Mid/Jr  │
 │Foundation│ │Business  │ │  Geo  │ │ Lab/AI   │ │Support  │
 └─────────┘ └──────────┘ └───────┘ └──────────┘ └─────────┘
 Domain+Infra  Auth+Patient  Nearby   OCR+Chat    SignalR+Tests
```

---

### 🔵 Dev 1 — Team Lead · Foundation & Architecture

**المسؤولية:** بناء طبقة Domain الكاملة + Infrastructure الأساسية + Auth foundation.
يبدأ أولاً ويمرج قبل باقي المطورين.

| File Path | Type | Description | Priority |
|-----------|------|-------------|----------|
| `Etmen.Domain/Common/BaseEntity.cs` | Entity | Id + CreatedAt + UpdatedAt + IsDeleted | 🔴 |
| `Etmen.Domain/Entities/User.cs` | Entity | Auth entity. Factory: `Create()`. `SetRefreshToken()` | 🔴 |
| `Etmen.Domain/Entities/PatientProfile.cs` | Entity | Vitals, BMI calc, Age calc, `UpdateMetrics()` | 🔴 |
| `Etmen.Domain/Entities/DoctorProfile.cs` | Entity | Specialty, License, Rating, IsAvailable | 🔴 |
| `Etmen.Domain/Entities/MedicalRecord.cs` | Entity | Vitals, `CreateFromLabResult()`, `AddDoctorNote()` | 🔴 |
| `Etmen.Domain/Entities/RiskAssessment.cs` | Entity | RiskScore VO, RiskLevel, `IsHighRisk()` | 🔴 |
| `Etmen.Domain/Entities/ChatMessage.cs` | Entity | Text, SenderRole, `MarkAsRead()` | 🟡 |
| `Etmen.Domain/Entities/Alert.cs` | Entity | AlertStatus enum, `MarkAsRead()`, `Dismiss()` | 🟡 |
| `Etmen.Domain/Entities/HealthcareProvider.cs` | Entity | GooglePlaceId, Type, GeoCoordinates — v3.0 🆕 | 🔴 |
| `Etmen.Domain/Entities/Appointment.cs` | Entity | SlotId, BookedAt, Status — v3.0 🆕 | 🔴 |
| `Etmen.Domain/Entities/AvailableSlot.cs` | Entity | DayOfWeek, StartTime, EndTime — v3.0 🆕 | 🟡 |
| `Etmen.Domain/Entities/LabResult.cs` | Entity | FileUrl, ExtractedValues, OcrStatus — v3.0 🆕 | 🔴 |
| `Etmen.Domain/Entities/FamilyLink.cs` | Entity | PrimaryUserId, LinkedUserId, Consent — v3.0 🆕 | 🔴 |
| `Etmen.Domain/Enums/RiskLevel.cs` | Enum | Low / Medium / High / Critical | 🔴 |
| `Etmen.Domain/Enums/UserRole.cs` | Enum | Patient / Doctor / Admin | 🔴 |
| `Etmen.Domain/Enums/PhysicalActivityLevel.cs` | Enum | Sedentary / Light / Moderate / Active / VeryActive | 🔴 |
| `Etmen.Domain/Enums/AlertStatus.cs` | Enum | Unread / Read / Dismissed | 🟡 |
| `Etmen.Domain/Enums/AnalysisPriority.cs` | Enum | Low / Medium / High / Urgent | 🟡 |
| `Etmen.Domain/ValueObjects/RiskScore.cs` | VO | Immutable 0.0–1.0, implicit double, `ToString()` % | 🔴 |
| `Etmen.Domain/ValueObjects/BloodPressure.cs` | VO | Systolic/Diastolic validation, `IsHypertensive` | 🔴 |
| `Etmen.Infrastructure/Persistence/AppDbContext.cs` | DbContext | 12 DbSets + soft-delete filters + configurations | 🔴 |
| `Etmen.Infrastructure/Persistence/Configurations/*.cs` | EF Config | 12 files — `IEntityTypeConfiguration<T>` per entity | 🟡 |
| `Etmen.Infrastructure/DependencyInjection.cs` | DI | Registers all repos + services via `AddInfrastructure()` | 🔴 |
| `Etmen.Infrastructure/Auth/JwtTokenService.cs` | Service | HS256 JWT + 64-byte refresh token | 🔴 |
| `Etmen.Infrastructure/Persistence/Repositories/UserRepository.cs` | Repo | GetByEmailAsync, EmailExistsAsync, CreateAsync | 🔴 |
| `Etmen.Application/Interfaces/IUserRepository.cs` | Interface | Contract for UserRepository | 🔴 |
| `Etmen.Application/Services/ITokenService.cs` | Interface | GenerateAccessToken, GenerateRefreshToken, ValidateToken | 🔴 |
| `Etmen.API/Program.cs` | API | Pipeline: ExceptionHandler, CORS, Auth, SignalR | 🔴 |
| `Etmen.API/Extensions/ServiceCollectionExtensions.cs` | API | AddJwtAuthentication, AddSwaggerWithJwt, AddApplicationServices | 🔴 |
| `Etmen.API/appsettings.json` | Config | All config keys (no real secrets) | 🔴 |
| `Etmen.API/appsettings.Development.json` | Config | Local dev overrides + SQL Express connection | 🔴 |

> ⏱️ **Estimated Duration:** 2.5 days
> ⚠️ **Note:** All other devs are **BLOCKED** until Dev 1 merges `feature/dev1-foundation` into `dev` branch.

---

### 🔷 Dev 2 — Senior Developer · Core Business Logic

**المسؤولية:** Auth system + Patient management + Medical records + Risk assessment + AI ML stub.
يبدأ بعد merge Dev 1.

| File Path | Type | Description | Priority |
|-----------|------|-------------|----------|
| `Etmen.Infrastructure/Persistence/Repositories/PatientRepository.cs` | Repo | GetByUserIdAsync, GetAllWithRiskAsync, UpdateAsync | 🔴 |
| `Etmen.Infrastructure/Persistence/Repositories/MedicalRecordRepository.cs` | Repo | GetByPatientIdAsync, GetLatestAsync, CreateAsync | 🔴 |
| `Etmen.Infrastructure/Persistence/Repositories/RiskAssessmentRepository.cs` | Repo | GetLatestByPatientAsync, GetHistoryAsync | 🔴 |
| `Etmen.Infrastructure/Persistence/Repositories/AlertRepository.cs` | Repo | GetUnreadByPatientAsync, MarkAllReadAsync | 🔴 |
| `Etmen.Application/Interfaces/IPatientRepository.cs` | Interface | 6 async methods with CancellationToken | 🔴 |
| `Etmen.Application/Interfaces/IMedicalRecordRepository.cs` | Interface | 5 async methods | 🔴 |
| `Etmen.Application/Interfaces/IRiskAssessmentRepository.cs` | Interface | 5 async methods | 🔴 |
| `Etmen.Application/Interfaces/IAlertRepository.cs` | Interface | 4 async methods | 🔴 |
| `Etmen.Application/UseCases/Auth/RegisterCommand.cs` | UseCase | Returns `Result<AuthResponse>`. Email conflict guard. | 🔴 |
| `Etmen.Application/UseCases/Auth/LoginQuery.cs` | UseCase | Returns `Result<AuthResponse>`. BCrypt verify. | 🔴 |
| `Etmen.Application/UseCases/Auth/RefreshTokenCommand.cs` | UseCase | Validate + rotate refresh token | 🔴 |
| `Etmen.Application/UseCases/Auth/GoogleLoginCommand.cs` | UseCase | Google ID token → JWT | 🔴 |
| `Etmen.Application/UseCases/Patient/GetPatientProfileQuery.cs` | UseCase | Full profile + current risk | 🔴 |
| `Etmen.Application/UseCases/Patient/UpdatePatientProfileCommand.cs` | UseCase | Update vitals → trigger risk recalc | 🔴 |
| `Etmen.Application/UseCases/Patient/GetHighRiskPatientsQuery.cs` | UseCase | Paginated. `PagedResult<PatientProfileResponse>` | 🔴 |
| `Etmen.Application/UseCases/Patient/GetPatientRiskHistoryQuery.cs` | UseCase | Time-series risk data | 🟡 |
| `Etmen.Application/UseCases/MedicalRecord/CreateMedicalRecordCommand.cs` | UseCase | Create record → trigger AI risk assessment | 🔴 |
| `Etmen.Application/UseCases/MedicalRecord/GetMedicalRecordQuery.cs` | UseCase | Single record + risk | 🟡 |
| `Etmen.Application/UseCases/MedicalRecord/AddDoctorNoteCommand.cs` | UseCase | Doctor adds clinical note | 🟡 |
| `Etmen.Application/UseCases/RiskAssessment/TriggerManualCommand.cs` | UseCase | Manual AI risk trigger outside schedule | 🔴 |
| `Etmen.Application/UseCases/RiskAssessment/GetAssessmentQuery.cs` | UseCase | Single assessment by ID | 🟡 |
| `Etmen.Application/UseCases/RiskAssessment/GetPatientAssessmentsQuery.cs` | UseCase | All assessments newest first | 🟡 |
| `Etmen.Application/UseCases/RiskAssessment/GetRequiredAnalysesQuery.cs` | UseCase | Recommended lab tests by risk | 🟡 |
| `Etmen.Application/UseCases/RiskAssessment/GetRecommendedDoctorsQuery.cs` | UseCase | Specialist recommendations | 🟡 |
| `Etmen.Infrastructure/AI/MLModelService.cs` | Service | **STUB** — returns score=0.75, High risk, version=stub | 🔴 |
| `Etmen.API/Controllers/AuthController.cs` | Controller | Register, Login, Refresh, Google — `Result<T>` pattern | 🔴 |
| `Etmen.API/Controllers/PatientController.cs` | Controller | Profile CRUD + high-risk list | 🔴 |
| `Etmen.API/Controllers/MedicalRecordController.cs` | Controller | Create + get + doctor note | 🔴 |
| `Etmen.API/Controllers/RiskAssessmentController.cs` | Controller | Assessments + trigger + analyses + recommended docs | 🔴 |
| `Etmen.API/Controllers/AdminController.cs` | Controller | Dashboard + high-risk patients | 🟡 |

> ⏱️ **Estimated Duration:** 3 days
> ⚠️ **ML Stub:** `MLModelService` يرجع قيم ثابتة. لا تكتب AI logic حقيقي — هذا Out of Scope.

```csharp
// MLModelService.cs — STUB implementation
public Task<RiskPredictionResult> PredictRiskAsync(
    RiskFeatures features, CancellationToken ct = default)
{
    return Task.FromResult(new RiskPredictionResult
    {
        Score        = new RiskScore(0.75),
        Level        = RiskLevel.High,
        ModelVersion = "stub-v0.1",
        Confidence   = 0.85,
    });
}
```

---

### 🟢 Dev 3 — Senior Developer · Nearby & Geo Services

**المسؤولية:** Google Maps integration + Nearby search + Appointment booking.
يعمل بالتوازي مع Dev 2.

| File Path | Type | Description | Priority |
|-----------|------|-------------|----------|
| `Etmen.Infrastructure/Persistence/Repositories/HealthcareProviderRepository.cs` | Repo | SearchNearbyAsync, GetWithSlotsAsync, UpsertFromGoogleAsync | 🔴 |
| `Etmen.Infrastructure/Persistence/Repositories/AppointmentRepository.cs` | Repo | BookSlotAsync, GetByPatientAsync, CancelAsync | 🔴 |
| `Etmen.Application/Interfaces/IHealthcareProviderRepository.cs` | Interface | 5 async methods | 🔴 |
| `Etmen.Application/Interfaces/IAppointmentRepository.cs` | Interface | 4 async methods | 🔴 |
| `Etmen.Infrastructure/Geo/GoogleGeoSearchService.cs` | Service | Google Places Nearby Search + MatchScore algorithm | 🔴 |
| `Etmen.Application/Services/IGeoSearchService.cs` | Interface | SearchProvidersAsync, GetProviderDetailsAsync, GeocodeAddressAsync | 🔴 |
| `Etmen.Application/UseCases/Nearby/GetNearbyProvidersQuery.cs` | UseCase | Combined doctors + hospitals by location | 🔴 |
| `Etmen.Application/UseCases/Nearby/GetNearbyDoctorsQuery.cs` | UseCase | Doctors filtered by specialty | 🔴 |
| `Etmen.Application/UseCases/Nearby/GetNearbyHospitalsQuery.cs` | UseCase | Hospitals within configurable radius | 🔴 |
| `Etmen.Application/UseCases/Nearby/GetProviderDetailsQuery.cs` | UseCase | Full provider details + slots | 🟡 |
| `Etmen.Application/UseCases/Nearby/BookAppointmentCommand.cs` | UseCase | Atomic slot booking with concurrency guard | 🔴 |
| `Etmen.API/Controllers/NearbyController.cs` | Controller | Nearby, details, book endpoints | 🔴 |
| `Etmen.Application/DTOs/Response/NearbyProviderResponse.cs` | DTO | Id, Name, Type, Distance, Rating, MatchScore, Slots | 🔴 |
| `Etmen.Application/DTOs/Response/ProviderDetails.cs` | DTO | Full details + available slots list | 🔴 |
| `Etmen.Application/DTOs/Request/BookAppointmentRequest.cs` | DTO | PatientId, ProviderId, SlotId | 🔴 |
| `Etmen.Application/DTOs/Internal/GeoSearchCriteria.cs` | DTO | Lat, Lng, RadiusMeters, Specialty, MaxResults | 🔴 |
| `Etmen.Application/DTOs/Internal/GeoCoordinate.cs` | DTO | Latitude, Longitude value type | 🔴 |

> 🗺️ **MatchScore Algorithm:**
> `MatchScore = Specialty(40%) + Distance(30%) + Rating(20%) + OpenNow+Slot(10%)`
> Distance decay: 1.0 at 0m → 0.0 at MaxRadius (linear). Rating: `provider.Rating / 5.0`

```csharp
// GoogleGeoSearchService.cs — MatchScore calculation
private static double CalculateMatchScore(
    NearbyProviderResponse provider, GeoSearchCriteria criteria)
{
    double specialtyScore = MatchSpecialty(provider, criteria.Specialty);   // 0.0–1.0
    double distanceScore  = 1.0 - (provider.DistanceMeters / criteria.MaxRadius);
    double ratingScore    = provider.Rating / 5.0;
    double availScore     = (provider.IsOpenNow ? 0.5 : 0) + (provider.HasSlots ? 0.5 : 0);

    return (specialtyScore * 0.40) + (distanceScore * 0.30)
         + (ratingScore   * 0.20) + (availScore    * 0.10);
}
```

> ⏱️ **Estimated Duration:** 3 days
> 🔑 **Key:** `GoogleMaps:ApiKey` يحتاج User Secrets. لا تضعه في appsettings committed.

---

### 🟠 Dev 4 — Mid-Level Developer · Lab OCR + Family + History + AI Chat

**المسؤولية:** Lab OCR extraction + Family multi-profile + Health history + AI patient chat.
يبدأ بعد merge Dev 1 + Dev 2 Auth.

| File Path | Type | Description | Priority |
|-----------|------|-------------|----------|
| **Lab OCR** | | | |
| `Etmen.Infrastructure/Persistence/Repositories/LabResultRepository.cs` | Repo | SaveAsync, GetByPatientAsync, UpdateOcrStatusAsync | 🔴 |
| `Etmen.Application/Interfaces/ILabResultRepository.cs` | Interface | 3 async methods | 🔴 |
| `Etmen.Infrastructure/AI/OcrService.cs` | Service | Tesseract / AzureCV. Extract lab values from PDF/image | 🔴 |
| `Etmen.Application/Services/IOcrService.cs` | Interface | `ExtractLabValuesAsync(IFormFile)` | 🔴 |
| `Etmen.Application/UseCases/Lab/UploadLabResultCommand.cs` | UseCase | OCR → extract → save → trigger risk recalc | 🔴 |
| `Etmen.API/Controllers/LabResultController.cs` | Controller | POST /upload, GET /patient/{id} | 🔴 |
| `Etmen.Application/DTOs/Response/ExtractedLabValues.cs` | DTO | BloodSugar, HbA1c, Cholesterol, HDL, LDL, Creatinine | 🔴 |
| **Family Linking** | | | |
| `Etmen.Infrastructure/Persistence/Repositories/FamilyLinkRepository.cs` | Repo | GetLinkedMembersAsync, CreateLinkAsync, DeleteLinkAsync | 🔴 |
| `Etmen.Application/Interfaces/IFamilyLinkRepository.cs` | Interface | 5 async methods | 🔴 |
| `Etmen.Infrastructure/Auth/FamilyService.cs` | Service | GenerateInviteToken, ValidateAndAccept, GenerateScopedToken | 🔴 |
| `Etmen.Application/Services/IFamilyService.cs` | Interface | 3 methods: invite, accept, scoped token | 🔴 |
| `Etmen.Application/UseCases/Family/InviteMemberCommand.cs` | UseCase | Generate invite token with 24h expiry | 🔴 |
| `Etmen.Application/UseCases/Family/AcceptInviteCommand.cs` | UseCase | Validate token + create FamilyLink | 🔴 |
| `Etmen.Application/UseCases/Family/GetFamilyMembersQuery.cs` | UseCase | List linked members + consent status | 🔴 |
| `Etmen.Application/UseCases/Family/SwitchProfileQuery.cs` | UseCase | Returns scoped JWT for linked member | 🔴 |
| `Etmen.Application/UseCases/Family/RemoveLinkCommand.cs` | UseCase | Delete FamilyLink. Both sides lose access. | 🟡 |
| `Etmen.API/Controllers/FamilyController.cs` | Controller | Invite, accept, list, switch, remove | 🔴 |
| **Health History** | | | |
| `Etmen.Application/UseCases/History/GetRiskHistoryQuery.cs` | UseCase | Time-series risk scores for chart | 🔴 |
| `Etmen.Application/UseCases/History/GetVitalsTimelineQuery.cs` | UseCase | BP + Sugar + BMI timeline data | 🔴 |
| `Etmen.API/Controllers/HealthHistoryController.cs` | Controller | Risk history + vitals timeline | 🔴 |
| **AI Chat** | | | |
| `Etmen.Infrastructure/AI/LlmPatientChatService.cs` | Service | Anthropic Claude API. System prompt with patient context | 🔴 |
| `Etmen.Application/Services/ILlmPatientChatService.cs` | Interface | `AskAsync(message, context, history)` | 🔴 |
| `Etmen.Application/UseCases/AIChat/AskPatientChatQuery.cs` | UseCase | Build context → call LLM → persist history | 🔴 |
| `Etmen.API/Controllers/AIChatController.cs` | Controller | POST /ask [Patient only] | 🔴 |
| `Etmen.Application/DTOs/Internal/PatientContext.cs` | DTO | PatientId, RiskLevel, PrimaryIssue, Vitals summary | 🔴 |
| `Etmen.Application/DTOs/Internal/ChatHistory.cs` | DTO | Max 10 messages. Oldest dropped on overflow. | 🟡 |
| `Etmen.Application/DTOs/Response/AIChatResponse.cs` | DTO | Message, Disclaimer, SourceReferences | 🔴 |

> ⏱️ **Estimated Duration:** 3 days
> 🔑 **Key:** `LlmChat:ApiKey` + `Ocr:ApiKey` يحتاجان User Secrets.

---

### 🟣 Dev 5 — Mid/Junior Developer · Chat, Notifications & Tests

**المسؤولية:** SignalR real-time chat + Notification services + Middleware + xUnit test suite.

| File Path | Type | Description | Priority |
|-----------|------|-------------|----------|
| `Etmen.Infrastructure/Persistence/Repositories/ChatRepository.cs` | Repo | SaveMessageAsync, GetConversationAsync, GetUnreadCountAsync | 🔴 |
| `Etmen.Application/Interfaces/IChatRepository.cs` | Interface | 4 async methods | 🔴 |
| `Etmen.Infrastructure/SignalR/ChatHub.cs` | Service | SendMessage, OnConnected, OnDisconnected groups | 🔴 |
| `Etmen.API/Controllers/ChatController.cs` | Controller | GET /history, GET /unread — REST fallback | 🔴 |
| `Etmen.Infrastructure/Notifications/EmailNotificationService.cs` | Service | SMTP TLS 587. Alert/appointment/risk notifications | 🔴 |
| `Etmen.Infrastructure/Notifications/PushNotificationService.cs` | Service | FCM push notifications. Skeleton. | 🟡 |
| `Etmen.API/Middleware/RequestLoggingMiddleware.cs` | Middleware | Log method + path + status + duration | 🔴 |
| `Etmen.API/Exceptions/GlobalExceptionHandler.cs` | Handler | .NET 8+ `IExceptionHandler`. Maps exceptions → ProblemDetails | 🔴 |
| `Etmen.API/Exceptions/ValidationExceptionHandler.cs` | Handler | FluentValidation → HTTP 422 per-field errors | 🔴 |
| `Etmen.Tests/Domain/EntityTests.cs` | Test | User, PatientProfile, MedicalRecord, RiskAssessment, Alert | 🔴 |
| `Etmen.Tests/Domain/ValueObjectTests.cs` | Test | RiskScore + BloodPressure edge cases | 🔴 |
| `Etmen.Tests/Application/AuthHandlerTests.cs` | Test | RegisterCommand + LoginQuery with Moq | 🔴 |
| `Etmen.Tests/Infrastructure/AppDbContextTests.cs` | Test | EF InMemory: all 12 DbSets + soft delete | 🔴 |
| `Etmen.Tests/API/AuthControllerTests.cs` | Test | WebApplicationFactory integration test | 🔴 |

> ⏱️ **Estimated Duration:** 2.5 days
> 🧪 **Tests:** Min 80% coverage on Domain entities. خدمات AI لا تتطلب unit tests — use integration tests.

---

<a id="section-2"></a>
## 🔄 Section 2: Layer Dependency Flow

### 2.1 Clean Architecture Diagram

```
┌──────────────────────────────────────────────────────────┐
│                      API Layer                           │
│   Program.cs  ·  Controllers  ·  Middleware  ·  Swagger  │
└─────────────────────────┬────────────────────────────────┘
                          │ depends on ↓
┌─────────────────────────▼────────────────────────────────┐
│                  Application Layer                       │
│   MediatR Handlers  ·  DTOs  ·  Interfaces  ·  Validators│
│   Result<T>  ·  PagedResult<T>  ·  Error types           │
└──────────┬──────────────────────────┬─────────────────────┘
           │ depends on ↓             │ depends on ↓
┌──────────▼─────────────┐  ┌─────────▼───────────────────┐
│   Infrastructure Layer │  │      Domain Layer            │
│   EF Core · Repos      │  │   Entities · Enums · VOs    │
│   JWT · SignalR · AI   │  │   Pure C# — zero deps       │
└────────────────────────┘  └─────────────────────────────┘
      Infrastructure also depends on Domain ──────────────►
```

### 2.2 Developer Dependency Table

| Developer | Depends On | Can Start After | Cannot Merge Before |
|-----------|-----------|-----------------|---------------------|
| Dev 1 — Lead | Nothing | Day 1 — immediate | n/a — Goes FIRST |
| Dev 2 — Senior | Dev 1 Domain + Infra | Dev 1 merges to dev branch | Dev 1 merge complete |
| Dev 3 — Senior | Dev 1 Domain + Infra | Dev 1 merges to dev branch | Dev 1 merge complete |
| Dev 4 — Mid | Dev 1 + Dev 2 Auth | Dev 1 merge + Auth use cases | Dev 1 + Dev 2 Auth merge |
| Dev 5 — Jr | All Layers (Tests) | After Dev 1 Domain is stable | All features merged |

> 📌 **Golden Rule — القاعدة الذهبية**
>
> **لا يُكتب كود حقيقي في طبقة تعتمد على طبقة لم تكتمل بعد.**
> مثال: Dev 2 لا يكتب `PatientRepository` قبل أن Domain entities تكون مرجوعة من Dev 1.
> الحل: استخدام `throw new NotImplementedException()` كـ placeholder حتى تكتمل الطبقة السابقة.

---

<a id="section-3"></a>
## 🌿 Section 3: Merge Points & Branch Strategy

### 3.1 Git Branch Tree

```
main (production)  ────────────────────────────────────────────────────►
   │
   └── dev (integration) ──────────────────────────────────────────────►
          │
          ├── feature/dev1-foundation          (Dev 1 — merges FIRST)
          │
          ├── feature/dev2-core-business       (Dev 2 — after Dev 1 merge)
          ├── feature/dev3-nearby-geo          (Dev 3 — after Dev 1 merge)
          │
          ├── feature/dev4-lab-family-ai       (Dev 4 — after Dev 1 + Dev 2 Auth)
          │
          └── feature/dev5-chat-notif-tests    (Dev 5 — after all above stable)
```

### 3.2 Merge Conditions Table

| Merge Point | Condition to Merge | Branches | Impact on Team |
|-------------|-------------------|----------|----------------|
| MP-1: Foundation | Domain compiles, EF migrations run, Swagger opens | `dev1-foundation → dev` | Unblocks Dev 2/3/4 |
| MP-2: Core Logic | Auth endpoints return 200/401/409, Risk calc works | `dev2-core-business → dev` | Unblocks Dev 4 AI chat |
| MP-3: Geo Search | Nearby returns providers, booking atomic | `dev3-nearby-geo → dev` | Unblocks final integration |
| MP-4: Lab + AI | OCR extracts values, AI chat returns response | `dev4-lab-family-ai → dev` | Unblocks full test suite |
| MP-5: Final Tests | All tests pass, coverage ≥ 80% Domain | `dev5-chat-notif-tests → dev` | Ready for main PR |

### 3.3 PR Requirements Checklist

```yaml
# .github/pull_request_template.md
pull_request_requirements:
  build:
    - dotnet build --no-restore → 0 errors
    - dotnet test → all pass
  code_quality:
    - No throw new NotImplementedException() in production paths
    - All new public types have XML <summary> doc
    - No hardcoded connection strings or API keys
    - No direct DbContext usage in Controllers
  architecture:
    - No new dependencies from Domain → Application/Infrastructure
    - No business logic in Controllers (MediatR only)
    - All async methods have CancellationToken ct = default
  security:
    - No secrets in appsettings.json committed
    - New endpoints have [Authorize(Roles = ...)]
  tests:
    - New domain entities have unit tests
    - New handlers have mock-based tests
```

### 3.4 Daily Git Workflow

```bash
# Start of day
git checkout dev
git pull origin dev
git checkout feature/dev{N}-your-feature
git rebase dev

# During work
git add -p                          # stage hunks selectively
git commit -m "feat(domain): add RiskScore value object with 0-1 validation"

# End of day push
git push origin feature/dev{N}-your-feature

# Before creating PR
dotnet build EtmenPlatform.sln
dotnet test Etmen.Tests/
git rebase dev  # resolve conflicts locally first
```

---

<a id="section-4"></a>
## ⚙️ Section 4: Configuration Setup

### 4.1 appsettings.Development.json — Full Config

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EtmenPlatformDB_Dev;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Issuer": "EtmenPlatformAPI",
    "Audience": "EtmenPlatformClients",
    "ExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  },
  "Google": {
    "ClientId": "REPLACE_WITH_USER_SECRET",
    "ClientSecret": "REPLACE_WITH_USER_SECRET"
  },
  "AIModel": {
    "Provider": "Stub",
    "ModelPath": "wwwroot/models/risk_model_v1.zip",
    "PythonServiceUrl": "http://localhost:8000"
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "From": "noreply@etmen.app"
  },
  "SignalR": {
    "MaximumReceiveMessageSize": 32768,
    "ClientTimeoutInterval": 30,
    "KeepAliveInterval": 15
  },
  "GoogleMaps": {
    "ApiKey": "REPLACE_WITH_USER_SECRET",
    "DefaultRadius": 5000,
    "MaxRadius": 25000,
    "Language": "ar"
  },
  "BlobStorage": {
    "Provider": "Local",
    "LocalPath": "wwwroot/uploads/lab-results"
  },
  "Ocr": {
    "Provider": "Tesseract",
    "ApiEndpoint": "",
    "ApiKey": "REPLACE_WITH_USER_SECRET"
  },
  "LlmChat": {
    "Provider": "Anthropic",
    "ApiKey": "REPLACE_WITH_USER_SECRET",
    "Model": "claude-sonnet-4-6",
    "MaxTokens": 800,
    "MaxHistory": 10
  },
  "Sms": {
    "Provider": "Vonage",
    "ApiKey": "REPLACE_WITH_USER_SECRET",
    "ApiSecret": "REPLACE_WITH_USER_SECRET",
    "FromNumber": "+201000000000",
    "Enabled": false
  },
  "NearbySearch": {
    "DefaultRadius": 5000,
    "MaxRadius": 25000,
    "MaxResults": 20,
    "CacheMinutes": 15
  }
}
```

### 4.2 Visual Studio 2026 Community Setup (8 Steps)

| Step | Action | Details |
|------|--------|---------|
| Step 1 | Open Solution | Double-click `EtmenPlatform.sln` → VS2026 opens with 5 projects |
| Step 2 | Restore NuGet | **Build → Restore NuGet Packages** (or `Ctrl+Shift+B`) |
| Step 3 | User Secrets | Right-click `Etmen.API` → **Manage User Secrets** → paste secrets (see Section 8) |
| Step 4 | Set Startup | Right-click `Etmen.API` → **Set as Startup Project** |
| Step 5 | Run Migrations | **Tools → NuGet Package Manager Console:** `Add-Migration InitialCreate -Project Etmen.Infrastructure -StartupProject Etmen.API` |
| Step 6 | Update DB | In PMC: `Update-Database -Project Etmen.Infrastructure -StartupProject Etmen.API` |
| Step 7 | Run F5 | Press **F5** → browser opens `https://localhost:7244/swagger` |
| Step 8 | Init Git | **Git → Create Git Repository** → `git remote add origin <repo-url>` |

### 4.3 NuGet Packages by Project

| Project | Package | Version | Purpose |
|---------|---------|---------|---------|
| `Etmen.Application` | `MediatR` | 12.4.1 | CQRS pipeline |
| `Etmen.Application` | `FluentValidation` | 11.11.0 | Request validation |
| `Etmen.Application` | `FluentValidation.DependencyInjectionExtensions` | 11.11.0 | Auto-register validators |
| `Etmen.Infrastructure` | `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.0 | SQL Server provider |
| `Etmen.Infrastructure` | `Microsoft.EntityFrameworkCore.Tools` | 9.0.0 | Migrations CLI |
| `Etmen.Infrastructure` | `BCrypt.Net-Next` | 4.0.3 | Password hashing |
| `Etmen.Infrastructure` | `Microsoft.IdentityModel.Tokens` | 8.3.2 | JWT signing keys |
| `Etmen.Infrastructure` | `System.IdentityModel.Tokens.Jwt` | 8.3.2 | JWT generation |
| `Etmen.API` | `Microsoft.AspNetCore.Authentication.JwtBearer` | 9.0.0 | JWT middleware |
| `Etmen.API` | `Microsoft.AspNetCore.Authentication.Google` | 9.0.0 | Google OAuth2 |
| `Etmen.API` | `Swashbuckle.AspNetCore` | 7.2.0 | Swagger UI |
| `Etmen.Tests` | `xunit` | 2.9.3 | Test framework |
| `Etmen.Tests` | `Moq` | 4.20.72 | Mocking |
| `Etmen.Tests` | `FluentAssertions` | 6.12.1 | Assertions |
| `Etmen.Tests` | `Microsoft.EntityFrameworkCore.InMemory` | 9.0.0 | In-memory DB |
| `Etmen.Tests` | `Microsoft.AspNetCore.Mvc.Testing` | 9.0.0 | Integration tests |

### 4.4 EF Migration Commands

```bash
# First migration
Add-Migration InitialCreate -Project Etmen.Infrastructure -StartupProject Etmen.API

# Apply to database
Update-Database -Project Etmen.Infrastructure -StartupProject Etmen.API

# Rollback if needed
Update-Database 0 -Project Etmen.Infrastructure -StartupProject Etmen.API

# List all migrations
Get-Migration -Project Etmen.Infrastructure -StartupProject Etmen.API
```

### 4.5 Git Initialization

```bash
git init
git remote add origin https://github.com/your-org/etmen-platform.git
git checkout -b dev
git add .
git commit -m "chore: initial scaffold — Et'men Platform v3.0"
git push -u origin dev

# Each developer creates their branch
git checkout -b feature/dev1-foundation
git checkout -b feature/dev2-core-business
git checkout -b feature/dev3-nearby-geo
git checkout -b feature/dev4-lab-family-ai
git checkout -b feature/dev5-chat-notif-tests
```

---

<a id="section-5"></a>
## 📐 Section 5: Coding Standards

### Rule 1 — Interface First

```csharp
// ✅ Correct
public interface IPatientRepository
{
    Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct);
}

// ❌ Wrong
public class PatientRepository
{
    // No interface — impossible to mock in tests
}
```

### Rule 2 — NotImplementedException for Skeleton

```csharp
// ✅ Correct — safe scaffold placeholder
public Task<PatientProfile?> GetByUserIdAsync(Guid id, CancellationToken ct)
    => throw new NotImplementedException();

// ❌ Wrong — silent null crashes at runtime with no trace
public Task<PatientProfile?> GetByUserIdAsync(Guid id, CancellationToken ct)
    => null!;
```

### Rule 3 — CancellationToken in All Async Methods

```csharp
// ✅ Correct
Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);

// ❌ Wrong
Task<User?> GetByEmailAsync(string email);
```

### Rule 4 — Factory Methods in Domain Entities

```csharp
// ✅ Correct — validation enforced in factory
public static User Create(string fullName, string email, string passwordHash, UserRole role)
{
    if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required.");
    return new User { Id = Guid.NewGuid(), FullName = fullName, Email = email, ... };
}

// ❌ Wrong — bypasses all validation
var user = new User { FullName = name, Email = email };
```

### Rule 5 — XML Comments on Every Public Type

```csharp
// ✅ Correct
/// <summary>
/// Validates RegisterRequest before it reaches the handler.
/// Enforces email format, password strength, and required fields.
/// </summary>
public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest> { }

// ❌ Wrong
public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest> { }
```

### Rule 6 — No Business Logic in Controllers

```csharp
// ✅ Correct — pure MediatR dispatch
public async Task<IActionResult> Register(RegisterRequest req, CancellationToken ct)
    => Ok(await _mediator.Send(new RegisterCommand(req), ct));

// ❌ Wrong — direct repo in controller
public async Task<IActionResult> Register(RegisterRequest req)
{
    var exists = await _userRepo.GetByEmailAsync(req.Email); // WRONG
}
```

### Rule 7 — Soft Delete with Global Query Filter

```csharp
// ✅ Correct — auto-excluded from ALL queries automatically
builder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

// ❌ Wrong — manual filter will be forgotten somewhere
var users = _db.Users.Where(u => !u.IsDeleted);
```

---

<a id="section-6"></a>
## 🚫 Section 6: Out of Scope

### 6.1 AI Training — Out of Scope

| AI Training Item | Status | Current Backend Alternative |
|-----------------|--------|------------------------------|
| Train ML diabetes risk model | 🚫 Out of Scope | `MLModelService` STUB returns score=0.75, RiskLevel.High |
| Custom NLP patient symptom parser | 🚫 Out of Scope | `LlmPatientChatService` calls Anthropic Claude API |
| Computer vision OCR model training | 🚫 Out of Scope | `OcrService` uses Tesseract local or Azure Computer Vision |
| Deploy AI model to production server | 🚫 Out of Scope | `PythonAIBridgeService.cs` sends HTTP to `localhost:8000` |
| Fine-tune LLM on medical Arabic data | 🚫 Out of Scope | System prompt injection with patient context JSON |

```csharp
// MLModelService.cs — STUB — لا تغير هذا الكود في الـ sprint الحالي
public Task<RiskPredictionResult> PredictRiskAsync(RiskFeatures features, CancellationToken ct = default)
{
    // TODO: Replace with real ML.NET model after AI team delivers trained model
    return Task.FromResult(new RiskPredictionResult
    {
        Score        = new RiskScore(0.75),
        Level        = RiskLevel.High,
        ModelVersion = "stub-v0.1",
        Confidence   = 0.85,
    });
}
```

### 6.2 Frontend UI — Out of Scope

| Frontend Item | Status | Backend Responsibility |
|--------------|--------|----------------------|
| React dashboard design/styling | 🚫 Out of Scope | Provide correct API response JSON structure |
| Mobile app (Flutter/React Native) | 🚫 Out of Scope | Provide correct API response JSON structure |
| Map UI / Google Maps rendering | 🚫 Out of Scope | Return lat/lng + metadata in `NearbyProviderResponse` |
| OCR file upload UI | 🚫 Out of Scope | Accept `IFormFile` in `/api/lab-results/upload` |
| AI Chat UI / WebSocket client | 🚫 Out of Scope | `ChatHub` SignalR server + REST fallback `/api/chat` |

---

<a id="section-7"></a>
## ✅ Section 7: Delivery Checklists

### Dev 1 — Team Lead Deliverables

- [ ] All 12 Domain entities compile with no errors
- [ ] All 5 Enums defined and match schema
- [ ] `RiskScore` VO throws `ArgumentOutOfRangeException` for < 0 or > 1
- [ ] `BloodPressure` VO throws for zero/negative values
- [ ] All entities inherit `BaseEntity` and have factory `Create()` methods
- [ ] `AppDbContext` has 12 DbSets + soft-delete `HasQueryFilter` on User + PatientProfile
- [ ] `ApplyConfigurationsFromAssembly()` called in `OnModelCreating`
- [ ] Initial EF Migration runs without errors (`Add-Migration InitialCreate`)
- [ ] `DependencyInjection.cs` registers all 18 services (check DI graph)
- [ ] `JwtTokenService.GenerateRefreshToken()` returns 64-byte base64 string
- [ ] `dotnet build EtmenPlatform.sln` → 0 errors, 0 warnings
- [ ] Swagger UI opens at `https://localhost:7244/swagger`
- [ ] `feature/dev1-foundation` merged to `dev` branch

### Dev 2 — Senior Core Business Deliverables

- [ ] `POST /api/auth/register` returns 201 with JWT for new email
- [ ] `POST /api/auth/register` returns 409 Conflict for duplicate email
- [ ] `POST /api/auth/login` returns 401 for wrong password
- [ ] `POST /api/auth/refresh` rotates token and invalidates old one
- [ ] `POST /api/auth/google` validates Google ID token
- [ ] `POST /api/medical-records/create` triggers `MLModelService` and returns risk
- [ ] `GET /api/patients/{id}` returns full profile with current risk
- [ ] `GET /api/admin/high-risk` returns `PagedResult<PatientProfileResponse>`
- [ ] `MLModelService` stub returns `RiskLevel.High` with score 0.75
- [ ] All 4 Auth use cases return `Result<AuthResponse>` (not raw exceptions)
- [ ] FluentValidation blocks register with weak password (< 8 chars, no digit)
- [ ] `dotnet test Etmen.Tests/` — AuthHandlerTests all pass
- [ ] All endpoints show correct `[Authorize(Roles=...)]` in Swagger

### Dev 3 — Senior Geo/Nearby Deliverables

- [ ] `GET /api/nearby/providers` returns list with MatchScore sorted descending
- [ ] MatchScore = 40% specialty + 30% distance + 20% rating + 10% openNow+slots
- [ ] Distance decay: 0.0 at MaxRadius (default 5000m), 1.0 at 0m
- [ ] `POST /api/nearby/book` creates appointment and marks slot `IsBooked=true`
- [ ] Concurrent booking attempts: second request returns 409 Conflict
- [ ] `GoogleMaps:ApiKey` loaded from User Secrets (never in appsettings committed)
- [ ] `GET /api/nearby/doctors` filters by `Specialty` query param
- [ ] `GET /api/nearby/hospitals` returns `Type=Hospital` providers only
- [ ] `GET /api/nearby/providers/{id}` returns full `ProviderDetails` with slots list
- [ ] `NearbyController` has `[Authorize(Roles = "Patient")]` on all endpoints
- [ ] `dotnet build` → 0 errors
- [ ] MatchScore calculation unit test passes

### Dev 4 — Mid-Level Deliverables

- [ ] `POST /api/lab-results/upload` accepts PDF and JPEG/PNG max 10MB
- [ ] `OcrService` extracts BloodSugar, HbA1c, Cholesterol from test PDF
- [ ] `UploadLabResultCommand` triggers new MedicalRecord + risk assessment
- [ ] `POST /api/family/invite` generates token with 24h expiry
- [ ] `POST /api/family/accept` creates `FamilyLink` for both users
- [ ] `GET /api/family/switch/{linkedUserId}` returns scoped JWT
- [ ] `GET /api/health-history/risk` returns time-series RiskScore array
- [ ] `GET /api/health-history/vitals` returns BP + Sugar + BMI timeline
- [ ] `POST /api/ai-chat/ask` returns AI response with medical disclaimer
- [ ] AI chat builds system prompt from `PatientContext` before each LLM call
- [ ] `LlmChat:ApiKey` loaded from User Secrets
- [ ] MaxHistory = 10: oldest message dropped when exceeded
- [ ] `dotnet build` → 0 errors

### Dev 5 — Mid/Junior Deliverables

- [ ] SignalR `ChatHub.SendMessage` persists to DB and pushes to recipient group
- [ ] `ChatHub.OnConnectedAsync` adds connection to userId group
- [ ] `GET /api/chat/history/{patientId}/{doctorId}` returns message list
- [ ] `EmailNotificationService` sends email on high-risk alert (verify with test SMTP)
- [ ] `PushNotificationService` skeleton compiles without errors
- [ ] `RequestLoggingMiddleware` logs: method + path + status + duration
- [ ] `GlobalExceptionHandler` returns 501 for `NotImplementedException`
- [ ] `ValidationExceptionHandler` returns 422 with per-field errors
- [ ] `EntityTests.cs`: all 20+ assertions pass with `dotnet test`
- [ ] `ValueObjectTests.cs`: RiskScore + BloodPressure edge cases pass
- [ ] `AppDbContextTests.cs`: InMemory DB persists User + MedicalRecord
- [ ] `AuthControllerTests.cs`: `GET /swagger` returns 200
- [ ] `dotnet test Etmen.Tests/` → all tests green
- [ ] Overall Domain test coverage ≥ 80%

---

<a id="section-8"></a>
## 🔐 Section 8: Security Warnings

### 8.1 User Secrets Setup

```bash
# Initialize user secrets (run once per developer machine)
cd Etmen.API
dotnet user-secrets init

# Set all secrets (replace VALUES with real ones)
dotnet user-secrets set "JwtSettings:SecretKey"        "your-256bit-min-secret-key-here"
dotnet user-secrets set "Google:ClientId"              "xxx.apps.googleusercontent.com"
dotnet user-secrets set "Google:ClientSecret"          "GOCSPX-xxx"
dotnet user-secrets set "GoogleMaps:ApiKey"            "AIzaSy-xxx"
dotnet user-secrets set "LlmChat:ApiKey"               "sk-ant-xxx"
dotnet user-secrets set "SmtpSettings:Password"        "your-smtp-app-password"
dotnet user-secrets set "Ocr:ApiKey"                   "your-azure-cv-key"
dotnet user-secrets set "Sms:ApiKey"                   "your-vonage-key"
dotnet user-secrets set "Sms:ApiSecret"                "your-vonage-secret"

# Verify all secrets set
dotnet user-secrets list
```

### 8.2 .gitignore Template

```gitignore
# Build & runtime
[Dd]ebug/
[Rr]elease/
[Bb]in/
[Oo]bj/
.vs/

# User Secrets — NEVER commit
**/secrets.json
appsettings.Production.json
appsettings.Secrets.json

# NuGet
*.nupkg
**/packages/
*.nuget.props

# Test results
TestResult.xml
coverage/

# Environment & uploads
.env
.env.local
wwwroot/uploads/
wwwroot/models/

# OS
.DS_Store
Thumbs.db
```

### 8.3 Production Security Checklist

- [ ] `JwtSettings:SecretKey` is minimum 256-bit (32 characters) in production
- [ ] HTTPS enforced — `UseHttpsRedirection()` + HSTS in `Program.cs`
- [ ] All secrets stored in Azure Key Vault (not appsettings in production)
- [ ] CORS restricted to production domain only (not `*`)
- [ ] Rate limiting enabled — 100 req/min per IP (`AddRateLimiter` in Program.cs)
- [ ] Passwords hashed with BCrypt cost factor ≥ 12
- [ ] Refresh token rotation on every use (old token invalidated immediately)
- [ ] JWT expiry = 15 minutes (not 24h — healthcare data is sensitive)
- [ ] `[Authorize(Roles = ...)]` on every non-public endpoint (no bare `[Authorize]`)
- [ ] SQL injection impossible — EF Core parameterized queries only (no raw SQL)
- [ ] File upload: ContentType whitelist + 10MB max enforced in FluentValidation
- [ ] Soft delete active — patient records never physically deleted
- [ ] All API responses use `ProblemDetails` format (no stack traces in production)
- [ ] Audit logs: `RequestLoggingMiddleware` logs all requests with TraceId

---

<a id="section-9"></a>
## 📞 Section 9: Full Endpoints Reference

### 9.1 Auth Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `POST` | `/api/auth/register` | Register new Patient or Doctor | ❌ | Public | |
| `POST` | `/api/auth/login` | Login with email + password | ❌ | Public | |
| `POST` | `/api/auth/refresh` | Rotate refresh token | ❌ | Public | |
| `POST` | `/api/auth/google` | Login via Google OAuth2 ID token | ❌ | Public | |

### 9.2 Patient Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `GET` | `/api/patients/{id}` | Get patient profile + current risk | ✅ | Patient/Doctor | |
| `PUT` | `/api/patients/{id}` | Update vitals → trigger risk recalc | ✅ | Patient | |
| `GET` | `/api/patients/high-risk` | Paginated high-risk patients list | ✅ | Doctor/Admin | |
| `GET` | `/api/patients/{id}/risk-history` | Time-series risk scores | ✅ | Patient/Doctor | |

### 9.3 Medical Record Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `POST` | `/api/medical-records` | Create record + trigger AI risk | ✅ | Patient/Doctor | |
| `GET` | `/api/medical-records/{id}` | Single record + risk assessment | ✅ | Patient/Doctor | |
| `PATCH` | `/api/medical-records/{id}/note` | Add doctor's clinical note | ✅ | Doctor | |

### 9.4 Risk Assessment Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `GET` | `/api/risk-assessments/{id}` | Single assessment | ✅ | Patient/Doctor | |
| `GET` | `/api/risk-assessments/patient/{id}` | All assessments newest first | ✅ | Patient/Doctor | |
| `POST` | `/api/risk-assessments/trigger` | Manual AI risk trigger | ✅ | Doctor/Admin | |
| `GET` | `/api/risk-assessments/{id}/analyses` | Required lab analyses | ✅ | Patient/Doctor | |
| `GET` | `/api/risk-assessments/{id}/doctors` | Recommended specialists | ✅ | Patient | |

### 9.5 Admin Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `GET` | `/api/admin/dashboard` | System KPIs + active patients | ✅ | Admin | |
| `GET` | `/api/admin/high-risk` | All high-risk patients | ✅ | Admin/Doctor | |

### 9.6 Nearby / Geo Endpoints — 🆕 v3.0

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `GET` | `/api/nearby/providers` | Nearby providers sorted by MatchScore | ✅ | Patient | 🆕 |
| `GET` | `/api/nearby/doctors` | Nearby doctors by specialty | ✅ | Patient | 🆕 |
| `GET` | `/api/nearby/hospitals` | Nearby hospitals | ✅ | Patient | 🆕 |
| `GET` | `/api/nearby/{id}` | Full provider details + slots | ✅ | Patient | 🆕 |
| `POST` | `/api/nearby/book` | Book appointment slot | ✅ | Patient | 🆕 |

### 9.7 Health History Endpoints

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `GET` | `/api/health-history/risk/{id}` | Risk score time-series for chart | ✅ | Patient/Doctor | |
| `GET` | `/api/health-history/vitals/{id}` | BP + Sugar + BMI timeline | ✅ | Patient/Doctor | |

### 9.8 Lab Result Endpoints — 🆕 v3.0

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `POST` | `/api/lab-results/upload` | Upload + OCR extract lab PDF/image | ✅ | Patient | 🆕 |
| `GET` | `/api/lab-results/{patientId}` | Patient's lab result history | ✅ | Patient/Doctor | 🆕 |

### 9.9 Family Link Endpoints — 🆕 v3.0

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `POST` | `/api/family/invite` | Generate family invite token | ✅ | Patient | 🆕 |
| `POST` | `/api/family/accept` | Accept invite + create link | ✅ | Patient | 🆕 |
| `GET` | `/api/family/members` | List linked family members | ✅ | Patient | 🆕 |
| `GET` | `/api/family/switch/{id}` | Get scoped JWT for linked member | ✅ | Patient | 🆕 |
| `DELETE` | `/api/family/{id}` | Remove family link | ✅ | Patient | 🆕 |

### 9.10 AI Chat Endpoints — 🆕 v3.0

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `POST` | `/api/ai-chat/ask` | Send message to AI with medical context | ✅ | Patient | 🆕 |

### 9.11 Real-Time Chat (SignalR + REST)

| Method | Route | Description | Auth | Role | v3.0 |
|--------|-------|-------------|------|------|------|
| `WS` | `/hubs/chat` | SignalR hub — JWT via `?access_token=` | ✅ | Doctor/Patient | |
| `GET` | `/api/chat/history` | Load chat history (REST fallback) | ✅ | Doctor/Patient | |
| `GET` | `/api/chat/unread` | Unread message count | ✅ | Doctor/Patient | |

---

*Et'men Platform v3.0 · Backend Team Workflow · March 2026*
*Clean Architecture · ASP.NET Core 9 · .NET 9 · EF Core 9 · MediatR 12 · FluentValidation 11*

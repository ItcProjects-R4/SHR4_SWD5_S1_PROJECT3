# دليل العمل الكامل للفريق التقني — Et'men Platform v3.0

> **الجمهور المستهدف:** فريق مكوّن من 5 مطورين Backend (قائد فريق + 4 أعضاء)  
> **الهدف:** بناء الـ Backend خطوة بخطوة بشكل متوازي ومنظّم  
> **خارج النطاق الحالي:** تدريب نماذج AI/ML — تصميم Frontend UI  
> **التقنية:** ASP.NET Core 10 · C# 12 · EF Core 10 · MediatR · SignalR · SQL Server

---

## 📋 فهرس المحتويات

1. [هيكل الفريق والمسؤوليات](#-هيكل-الفريق-والمسؤوليات)
2. [تدفق التبعيات بين الطبقات](#-تدفق-التبعيات-بين-الطبقات)
3. [خريطة العمل التفصيلية لكل مطور](#-خريطة-العمل-التفصيلية-لكل-مطور)
4. [نقاط الـ Merge والـ Branch Strategy](#-نقاط-الـ-merge-والـ-branch-strategy)
5. [الإعدادات والـ Configuration الأساسية](#-الإعدادات-والـ-configuration-الأساسية)
6. [قواعد الكود والمعايير](#-قواعد-الكود-والمعايير)
7. [ما هو خارج النطاق الحالي](#-ما-هو-خارج-النطاق-الحالي)
8. [Checklist التسليم لكل مطور](#-checklist-التسليم-لكل-مطور)

---

## 👥 هيكل الفريق والمسؤوليات

```
┌─────────────────────────────────────────────────────────────────┐
│                    Et'men Platform v3.0                         │
│                     Backend Team (5 Devs)                       │
├─────────────────────────────────────────────────────────────────┤
│  Dev 1 — Team Lead     │ Domain + DB + Auth + DI Foundation     │
│  Dev 2 — Senior        │ Core Business Logic (Patient/Record/Risk)│
│  Dev 3 — Senior        │ Nearby + Geo + Appointments (v3.0)     │
│  Dev 4 — Mid-Level     │ Lab OCR + Family + History (v3.0)      │
│  Dev 5 — Mid/Junior    │ Chat/SignalR + Notifications + Tests    │
└─────────────────────────────────────────────────────────────────┘
```

### Dev 1 — Team Lead (يبدأ أولاً - الأساس الذي يعتمد عليه الجميع)

**المسؤولية:** بناء العمود الفقري للمشروع — كل المطورين الآخرين لا يمكنهم البدء بشكل كامل حتى ينتهي من هذه المرحلة.

**الملفات المطلوبة:**
- `Etmen.Domain/` — كل الـ Entities والـ Enums والـ Value Objects
- `Etmen.Infrastructure/Persistence/AppDbContext.cs`
- `Etmen.Infrastructure/Persistence/Configurations/` — كل الـ EF Configurations (12 ملف)
- `Etmen.Infrastructure/DependencyInjection.cs` — تسجيل كل الـ services
- `Etmen.Infrastructure/Auth/JwtTokenService.cs` — تنفيذ كامل (ليس NotImplemented)
- `Etmen.Application/Services/ITokenService.cs`
- `Etmen.Application/Interfaces/IUserRepository.cs`
- `Etmen.Infrastructure/Persistence/Repositories/UserRepository.cs` — تنفيذ كامل
- `Etmen.API/Extensions/ServiceCollectionExtensions.cs`
- `Etmen.API/Program.cs`
- `appsettings.json` + `appsettings.Development.json` — بكل الـ keys المطلوبة
- إعداد Initial Migration في EF Core

**المدة المقدّرة:** 3-4 أيام عمل

---

### Dev 2 — Senior (يبدأ بعد Dev 1 يُسلّم Domain Layer)

**المسؤولية:** تنفيذ Core Business Logic — السيناريوهات A و B و C من الديكومنتاشن.

**الملفات المطلوبة:**
- `Etmen.Infrastructure/Persistence/Repositories/PatientRepository.cs`
- `Etmen.Infrastructure/Persistence/Repositories/MedicalRecordRepository.cs`
- `Etmen.Infrastructure/Persistence/Repositories/RiskAssessmentRepository.cs`
- `Etmen.Infrastructure/Persistence/Repositories/AlertRepository.cs`
- `Etmen.Application/UseCases/Auth/` — تنفيذ كامل للـ Handlers الأربعة
- `Etmen.Application/UseCases/Patient/` — تنفيذ كامل للـ Handlers الأربعة
- `Etmen.Application/UseCases/MedicalRecord/` — تنفيذ كامل للـ 3 Handlers
- `Etmen.Application/UseCases/RiskAssessment/` — تنفيذ كامل للـ 5 Handlers
- `Etmen.API/Controllers/AuthController.cs` — تنفيذ كامل
- `Etmen.API/Controllers/PatientController.cs` — تنفيذ كامل
- `Etmen.API/Controllers/MedicalRecordController.cs` — تنفيذ كامل
- `Etmen.API/Controllers/RiskAssessmentController.cs` — تنفيذ كامل
- `Etmen.API/Controllers/AdminController.cs` — تنفيذ كامل
- `Etmen.Infrastructure/AI/MLModelService.cs` — تنفيذ كل الـ methods ما عدا الـ ML model loading (استخدم placeholder يرجع قيمة ثابتة)

**ملاحظة مهمة:** لا تنفيذ فعلي لنموذج AI الآن — استخدم Stub يرجع:
```csharp
// STUB — يُستبدل لاحقاً بالـ ML.NET model الحقيقي
return Task.FromResult(new RiskPredictionResult
{
    Score = 0.75, // placeholder
    RiskLevel = RiskLevel.High,
    ModelVersion = "stub-v0.1"
});
```

**المدة المقدّرة:** 5-6 أيام عمل

---

### Dev 3 — Senior (يعمل بالتوازي مع Dev 2 بعد Dev 1)

**المسؤولية:** ميزات v3.0 المرتبطة بالموقع الجغرافي والحجز.

**الملفات المطلوبة:**
- `Etmen.Infrastructure/Persistence/Repositories/HealthcareProviderRepository.cs`
- `Etmen.Infrastructure/Persistence/Repositories/AppointmentRepository.cs`
- `Etmen.Infrastructure/Geo/GoogleGeoSearchService.cs` — تنفيذ كامل (HTTP calls لـ Google Places API)
- `Etmen.Application/UseCases/Nearby/GetNearbyProvidersQuery.cs` — Handler كامل
- `Etmen.Application/UseCases/Nearby/GetNearbyDoctorsQuery.cs` — Handler كامل
- `Etmen.Application/UseCases/Nearby/GetNearbyHospitalsQuery.cs` — Handler كامل
- `Etmen.Application/UseCases/Nearby/GetProviderDetailsQuery.cs` — Handler كامل
- `Etmen.Application/UseCases/Nearby/BookAppointmentCommand.cs` — Handler كامل
- `Etmen.API/Controllers/NearbyController.cs` — تنفيذ كامل
- خوارزمية MatchScore في `GoogleGeoSearchService.cs`:
  ```
  Specialty Match  40%
  Distance         30%
  Rating           20%
  Open Now + Slot  10%
  ```

**المدة المقدّرة:** 4-5 أيام عمل

---

### Dev 4 — Mid-Level (يعمل بالتوازي مع Dev 2 و Dev 3 بعد Dev 1)

**المسؤولية:** ميزات v3.0 الثلاث المتبقية.

**الملفات المطلوبة:**

**Lab OCR:**
- `Etmen.Infrastructure/Persistence/Repositories/LabResultRepository.cs`
- `Etmen.Infrastructure/AI/OcrService.cs` — تنفيذ كامل (Tesseract.NET أو Azure CV)
- `Etmen.Application/UseCases/Lab/UploadLabResultCommand.cs` — Handler كامل (Pipeline الكامل: Store → OCR → CreateMedicalRecord → ReScore → Notify)
- `Etmen.API/Controllers/LabResultController.cs` — تنفيذ كامل

**Family Linking:**
- `Etmen.Infrastructure/Persistence/Repositories/FamilyLinkRepository.cs`
- `Etmen.Application/Services/IFamilyService.cs` — تنفيذ Implementation
- `Etmen.Application/UseCases/Family/` — تنفيذ كامل للـ 5 Handlers
- `Etmen.API/Controllers/FamilyController.cs` — تنفيذ كامل

**Health History:**
- `Etmen.Application/UseCases/History/GetRiskHistoryQuery.cs` — Handler كامل
- `Etmen.Application/UseCases/History/GetVitalsTimelineQuery.cs` — Handler كامل
- `Etmen.API/Controllers/HealthHistoryController.cs` — تنفيذ كامل

**AI Chat:**
- `Etmen.Infrastructure/AI/LlmPatientChatService.cs` — تنفيذ كامل (HTTP calls لـ Claude/GPT API)
- `Etmen.Application/UseCases/AIChat/AskPatientChatQuery.cs` — Handler كامل
- `Etmen.API/Controllers/AIChatController.cs` — تنفيذ كامل

**المدة المقدّرة:** 6-7 أيام عمل

---

### Dev 5 — Mid/Junior (يبدأ بعد Dev 1 — يدعم الفريق بالكامل)

**المسؤولية:** البنية التحتية الداعمة: Chat/SignalR, Notifications, Logging, Tests.

**الملفات المطلوبة:**

**Chat & SignalR:**
- `Etmen.Infrastructure/Persistence/Repositories/ChatRepository.cs` — تنفيذ كامل
- `Etmen.Infrastructure/SignalR/ChatHub.cs` — تنفيذ كامل
- `Etmen.API/Controllers/ChatController.cs` — تنفيذ كامل

**Notifications:**
- `Etmen.Infrastructure/Notifications/EmailNotificationService.cs` — تنفيذ كامل (SMTP + Vonage SMS)
- `Etmen.Infrastructure/Notifications/PushNotificationService.cs` — تنفيذ كامل

**Middleware:**
- `Etmen.API/Middleware/ExceptionHandlingMiddleware.cs` — تنفيذ كامل
- `Etmen.API/Middleware/RequestLoggingMiddleware.cs` — تنفيذ كامل

**Tests:**
- `EarlyIntervention.UnitTests/` — إعداد xUnit + Moq + FluentAssertions
- Unit tests لـ Domain entities (RiskAssessment, User, MedicalRecord)
- Unit tests لـ Value Objects (RiskScore, BloodPressure)
- Integration test skeleton لـ AuthController

**المدة المقدّرة:** 4-5 أيام عمل

---

## 🔄 تدفق التبعيات بين الطبقات

```
Domain Layer (Entities + Enums + Value Objects)
        ↓  [يكمله Dev 1 أولاً — لا يعتمد على أي طبقة أخرى]
        ↓
Infrastructure Layer (Repositories + DB + Auth)
        ↓  [يبدأ بعد Domain — يحتاج Entities لتعريف DbSets]
        ↓
Application Layer (Use Cases + Service Interfaces)
        ↓  [يبدأ بعد Interfaces — ينفّذ الـ Handlers]
        ↓
API Layer (Controllers + Middleware + Program.cs)
             [يبدأ بعد Use Cases — يحتاج Commands/Queries]
```

**قاعدة صارمة:** لا يُكتب كود حقيقي في طبقة تعتمد على طبقة لم تكتمل بعد. استخدم `throw new NotImplementedException()` كـ placeholder حتى تكتمل الطبقة السابقة.

---

## 🌿 نقاط الـ Merge والـ Branch Strategy

```
main
  └── dev (integration branch)
        ├── feature/dev1-domain-foundation
        ├── feature/dev2-core-business-logic
        ├── feature/dev3-nearby-geo
        ├── feature/dev4-lab-family-history-ai
        └── feature/dev5-chat-notifications-tests
```

### قواعد الـ Merge:

| Merge Point | الشرط المطلوب |
|-------------|---------------|
| **Merge 1** | Dev 1 يُكمل Domain + DbContext + DI → يُدمج في `dev` → يبدأ الجميع |
| **Merge 2** | Dev 2 يُكمل Core Repos + Auth UseCases → يُدمج في `dev` |
| **Merge 3** | كل من Dev 3 و Dev 4 و Dev 5 يُكملون features موازية → Merge متزامن في `dev` |
| **Merge 4** | بعد Integration Testing الكامل → `dev` → `main` |

**Dev 2 لا يستطيع Merge** حتى يُكمل Dev 1 Domain Layer ويُدمجها.  
**Dev 3 و Dev 4 يمكنهم العمل بشكل متوازٍ** بعد Merge 1 لأنهم يعتمدون فقط على الـ Interfaces.

---

## ⚙️ الإعدادات والـ Configuration الأساسية

**هذا القسم يجب أن يُعدّه Dev 1 قبل أي شيء آخر.**

### appsettings.Development.json (للبيئة المحلية)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EtmenPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "CHANGE_THIS_TO_A_256_BIT_SECRET_MIN_32_CHARS_LONG",
    "Issuer": "EtmenPlatformAPI",
    "Audience": "EtmenPlatformClients",
    "ExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  },
  "Google": {
    "ClientId": "your-google-client-id.apps.googleusercontent.com",
    "ClientSecret": "your-google-client-secret"
  },
  "AIModel": {
    "Provider": "Stub",
    "ModelPath": "wwwroot/models/risk_model_v1.zip",
    "PythonServiceUrl": "http://localhost:8000"
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "User": "noreply@etmen.app",
    "Password": "your-app-password",
    "From": "Et'men Platform <noreply@etmen.app>"
  },
  "SignalR": {
    "HubPath": "/hubs/chat"
  },
  "GoogleMaps": {
    "ApiKey": "AIzaSy_YOUR_KEY_HERE",
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
    "ApiKey": ""
  },
  "LlmChat": {
    "Provider": "Anthropic",
    "ApiKey": "sk-ant-YOUR_KEY_HERE",
    "Model": "claude-sonnet-4-6",
    "MaxTokens": 800,
    "MaxHistory": 10
  },
  "Sms": {
    "Provider": "Vonage",
    "ApiKey": "your-vonage-key",
    "ApiSecret": "your-vonage-secret",
    "FromNumber": "+201000000000",
    "Enabled": false
  }
}
```

### إعداد السولوشن بـ Visual Studio 2026 Community

> **المتطلبات:** Visual Studio 2026 Community مثبّت مع Workload: **ASP.NET and web development**

---

#### الخطوة 1 — إنشاء السولوشن الفارغ

1. افتح **Visual Studio 2026 Community**
2. من الشاشة الرئيسية اختر **Create a new project**
3. في مربع البحث اكتب: `Blank Solution`
4. اختر **Blank Solution** ← اضغط **Next**
5. في حقل **Solution name** اكتب: `EtmenPlatform`
6. اختر مجلد الحفظ ← اضغط **Create**

---

#### الخطوة 2 — إضافة المشاريع الأربعة

افتح **Solution Explorer** (View → Solution Explorer)، اضغط كليك يمين على اسم السولوشن ← **Add → New Project** وكرّر لكل مشروع:

| # | Project Name | Template المطلوب | Framework |
|---|---|---|---|
| 1 | `Etmen.Domain` | **Class Library** (C#) | .NET 10.0 |
| 2 | `Etmen.Application` | **Class Library** (C#) | .NET 10.0 |
| 3 | `Etmen.Infrastructure` | **Class Library** (C#) | .NET 10.0 |
| 4 | `Etmen.API` | **ASP.NET Core Web API** (C#) | .NET 10.0 |

> لـ `Etmen.API`: تأكد أن **Use controllers** مفعّل وأن **Enable OpenAPI support** مفعّل

---

#### الخطوة 3 — إضافة Project References

في **Solution Explorer**، لكل مشروع: كليك يمين ← **Add → Project Reference**:

| المشروع | يعتمد على |
|---|---|
| `Etmen.Application` | ✅ `Etmen.Domain` |
| `Etmen.Infrastructure` | ✅ `Etmen.Domain` + ✅ `Etmen.Application` |
| `Etmen.API` | ✅ `Etmen.Application` + ✅ `Etmen.Infrastructure` |

---

#### الخطوة 4 — تثبيت NuGet Packages

افتح **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console):

```powershell
# ── Etmen.Infrastructure ──────────────────────────────────────────
Install-Package Microsoft.EntityFrameworkCore.SqlServer         -ProjectName Etmen.Infrastructure
Install-Package Microsoft.EntityFrameworkCore.Tools             -ProjectName Etmen.Infrastructure
Install-Package Microsoft.EntityFrameworkCore.Design            -ProjectName Etmen.Infrastructure
Install-Package Microsoft.AspNetCore.SignalR.Core               -ProjectName Etmen.Infrastructure
Install-Package BCrypt.Net-Next                                 -ProjectName Etmen.Infrastructure
Install-Package Microsoft.IdentityModel.Tokens                  -ProjectName Etmen.Infrastructure
Install-Package System.IdentityModel.Tokens.Jwt                 -ProjectName Etmen.Infrastructure
Install-Package Microsoft.AspNetCore.Http.Abstractions          -ProjectName Etmen.Infrastructure

# ── Etmen.Application ─────────────────────────────────────────────
Install-Package MediatR                                         -ProjectName Etmen.Application
Install-Package Microsoft.AspNetCore.Http.Abstractions          -ProjectName Etmen.Application

# ── Etmen.API ─────────────────────────────────────────────────────
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer   -ProjectName Etmen.API
Install-Package Microsoft.AspNetCore.Authentication.Google      -ProjectName Etmen.API
Install-Package Swashbuckle.AspNetCore                          -ProjectName Etmen.API
```

---

#### الخطوة 5 — إنشاء أول EF Migration

في نفس **Package Manager Console** (تأكد أن Startup Project هو `Etmen.API`):

```powershell
# كليك يمين على Etmen.API ← Set as Startup Project  ثم:
Add-Migration InitialCreate -Project Etmen.Infrastructure -StartupProject Etmen.API
Update-Database             -Project Etmen.Infrastructure -StartupProject Etmen.API
```

---

#### الخطوة 6 — إعداد User Secrets (بدلاً من كتابة secrets في appsettings)

كليك يمين على `Etmen.API` ← **Manage User Secrets** ← أضف:

```json
{
  "JwtSettings:SecretKey": "CHANGE_THIS_TO_A_256_BIT_SECRET_MIN_32_CHARS",
  "LlmChat:ApiKey":        "sk-ant-YOUR_ANTHROPIC_KEY",
  "GoogleMaps:ApiKey":     "AIzaSy_YOUR_GOOGLE_MAPS_KEY",
  "Google:ClientId":       "your-google-client-id.apps.googleusercontent.com",
  "Google:ClientSecret":   "your-google-client-secret",
  "Sms:ApiSecret":         "your-vonage-secret"
}
```

---

#### الخطوة 7 — تشغيل المشروع

1. تأكد أن `Etmen.API` هو **Startup Project** (اسمه bold في Solution Explorer)
2. اضغط **F5** أو زر ▶️ الأخضر
3. سيفتح المتصفح على: `https://localhost:{port}/swagger`
4. تحقق أن كل الـ Controllers تظهر في Swagger UI

---

#### الخطوة 8 — إعداد Git من داخل Visual Studio 2026

1. من القائمة: **Git → Create Git Repository**
2. اختر **Local only** أو اربط بـ GitHub/Azure DevOps مباشرةً
3. تأكد أن `.gitignore` يشمل: `**/bin/` و `**/obj/` و `*.user` و `appsettings.Production.json`

> لإضافة Remote: **Git → Manage Remotes** → أضف URL الـ repository

---

## 📐 قواعد الكود والمعايير

### 1. Interface First — دائماً
```csharp
// ✅ صح — تعريف الـ Interface أولاً في Application Layer
public interface IPatientRepository { ... }

// ✅ ثم التنفيذ في Infrastructure Layer
public class PatientRepository : IPatientRepository { ... }

// ❌ خطأ — لا تكتب Implementation بدون Interface
public class PatientRepository { ... }
```

### 2. NotImplementedException للـ Skeleton
```csharp
// ✅ كل الـ methods تبدأ هكذا حتى تُنفَّذ
public Task<PatientProfile?> GetByIdAsync(Guid id, CancellationToken ct = default)
    => throw new NotImplementedException();
```

### 3. CancellationToken في كل الـ Async Methods
```csharp
// ✅ صح
Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);

// ❌ خطأ
Task<User?> GetByEmailAsync(string email);
```

### 4. Factory Methods في Domain Entities
```csharp
// ✅ صح — استخدم static factory method
var user = User.Create(fullName, email, passwordHash, UserRole.Patient);

// ❌ خطأ — لا تستخدم constructor مباشرةً خارج الـ Domain
var user = new User { FullName = fullName, ... };
```

### 5. XML Comments على كل Class و Interface
```csharp
/// <summary>
/// Repository contract for PatientProfile queries and updates.
/// </summary>
public interface IPatientRepository { ... }
```

### 6. لا Business Logic في Controllers
```csharp
// ✅ صح — Controller يوفّر فقط للـ MediatR
[HttpPost]
public async Task<IActionResult> CreateRecordAsync(
    [FromBody] CreateMedicalRecordRequest request, CancellationToken ct)
    => Ok(await _mediator.Send(new CreateMedicalRecordCommand(request), ct));

// ❌ خطأ — لا تضع logic في Controller
[HttpPost]
public async Task<IActionResult> CreateRecordAsync([FromBody] CreateMedicalRecordRequest request)
{
    var record = new MedicalRecord(...); // ❌ خطأ
    _db.Add(record);                     // ❌ خطأ
    await _db.SaveChangesAsync();        // ❌ خطأ
}
```

---

## 🚫 ما هو خارج النطاق الحالي

### 1. تدريب نموذج AI (ML.NET / Python)
تدريب وتقييم نموذج الـ ML خارج نطاق هذه المرحلة تماماً. كل Dev يستخدم `MLModelService` كـ Stub يرجع قيمة ثابتة:

```csharp
// في MLModelService.cs — STUB مؤقت
public Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features, CancellationToken ct)
    => Task.FromResult(new RiskPredictionResult
    {
        Score        = 0.75,
        RiskLevel    = RiskLevel.High,
        ModelVersion = "stub-v0.1-placeholder",
        PrimaryIssue = "Elevated Blood Sugar (Stub)"
    });
```

**متى يُنفّذ:** يُنفَّذ بواسطة متخصص Data Scientist في مرحلة منفصلة.

### 2. تصميم واجهة Frontend UI
الـ Frontend موجود في المستودع كـ Skeleton فقط (ملفات `.jsx` فارغة). **لا يُطلب من أي Backend developer تصميم UI.**

الـ Backend developers مسؤولون فقط عن:
- تعريف API contracts صحيحة (DTOs)
- توثيق كل endpoint في Swagger
- إرجاع Response shapes صحيحة ومتسقة

---

## ✅ Checklist التسليم لكل مطور

### Dev 1 — يُسلّم:
- [ ] كل الـ Domain Entities تبني بدون أخطاء `dotnet build`
- [ ] كل الـ Enums و Value Objects موجودة
- [ ] `AppDbContext` يحتوي على 12 DbSet
- [ ] كل الـ EF Configurations موجودة (ولو بـ `throw new NotImplementedException`)
- [ ] `DependencyInjection.cs` يُسجّل كل الـ services
- [ ] `JwtTokenService` يعمل فعلياً (GenerateAccessToken + RefreshToken)
- [ ] `UserRepository` يعمل فعلياً (GetByEmail, Create)
- [ ] Initial EF Migration موجود ويُطبّق بنجاح
- [ ] `appsettings.Development.json` مُعدّ بكل الـ keys

### Dev 2 — يُسلّم:
- [ ] كل الـ Core Repositories تعمل (Patient, MedicalRecord, RiskAssessment, Alert)
- [ ] Auth flow كامل: Register → Login → RefreshToken يعملوا end-to-end
- [ ] `CreateMedicalRecord` endpoint يعمل (حتى لو يرجع stub risk score)
- [ ] AdminDashboard يرجع بيانات حقيقية من DB
- [ ] كل الـ Controllers تظهر في Swagger

### Dev 3 — يُسلّم:
- [ ] `GoogleGeoSearchService` يتصل فعلياً بـ Google Places API
- [ ] `GET /api/nearby/providers` يرجع نتائج حقيقية مرتّبة بـ MatchScore
- [ ] `POST /api/nearby/book` يحجز slot ويحدّث الـ DB

### Dev 4 — يُسلّم:
- [ ] `POST /api/lab-results/upload` يُشغّل OCR ويُنشئ MedicalRecord جديد
- [ ] `POST /api/family/invite` يُرسل email مع invite link
- [ ] `GET /api/family/switch/{id}` يُعيد scoped JWT
- [ ] `GET /api/health-history/risk` يرجع بيانات مرتّبة بالتاريخ
- [ ] `POST /api/ai-chat/ask` يتصل بـ LLM API ويرجع رد

### Dev 5 — يُسلّم:
- [ ] SignalR ChatHub يعمل (connect + send + receive)
- [ ] `POST /api/chat/send` يحفظ رسالة ويُرسلها real-time
- [ ] Email notifications تعمل (SMTP)
- [ ] Exception middleware يمسك كل الـ exceptions ويرجع JSON
- [ ] Request logging يُسجّل كل الـ requests
- [ ] 10+ Unit tests تمر بنجاح

---

## 🔐 تحذيرات الأمان المهمة

> **⚠️ لا تُضيف أي Secret حقيقي في Git تحت أي ظرف**

```bash
# استخدم dotnet user-secrets للـ local development
dotnet user-secrets set "JwtSettings:SecretKey" "your-super-secret-key" --project Etmen.API
dotnet user-secrets set "LlmChat:ApiKey"        "sk-ant-your-key"       --project Etmen.API
dotnet user-secrets set "GoogleMaps:ApiKey"     "AIzaSy-your-key"       --project Etmen.API
```

أضف `.gitignore` يشمل:
```
appsettings.Production.json
appsettings.Secrets.json
*.user
```

---

*Et'men Platform v3.0 — Backend Workflow README*  
*آخر تحديث: 2025*

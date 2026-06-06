# Detailed User Stories to Controller Actions Mapping

## 🔐 4.1 GUESTS

### Story: Register new account
- **Controller**: `AccountController.cs`
- **Action**: `Register(GET)` → Form
- **Action**: `Register(POST)` → Submit registration
- **ViewModel**: `RegisterViewModel`
- **Features**: Email validation, password strength

### Story: Verify email using link
- **Controller**: `AccountController.cs`
- **Action**: `VerifyEmail(token)` → Email verification endpoint
- **View**: `VerifyEmailNotice.cshtml`
- **Features**: Token validation, email confirmation

### Story: Request password recovery
- **Controller**: `AccountController.cs`
- **Action**: `ForgotPassword(GET)` → Form
- **Action**: `ForgotPassword(POST)` → Send reset link
- **Action**: `ResetPassword(token, GET)` → Reset form
- **Action**: `ResetPassword(POST)` → Update password
- **ViewModels**: `ForgotPasswordViewModel`, `ResetPasswordViewModel`

### Story: View landing pages & crisis mode
- **Controller**: `HomeController.cs`
- **Action**: `Index()` → Landing page
- **ViewModel**: `LandingPageViewModel` (with crisis status)
- **Features**: Crisis mode banner, public health info

---

## 👨‍⚕️ 4.2 PATIENTS (15 Stories)

### Story 1: View dashboard & status
- **Controller**: `PatientDashboardController.cs`
- **Action**: `Index(GET)` → Dashboard home
- **ViewModel**: `PatientDashboardViewModel`
- **Features**: Latest health status, upcoming appointments, notifications

### Story 2: Update health profile
- **Controller**: `PatientProfileController.cs`
- **Action**: `Edit(GET)` → Edit form
- **Action**: `Edit(POST)` → Save changes
- **ViewModel**: `PatientProfileViewModel`
- **Fields**: Height, weight, chronic conditions, allergies, medications

### Story 3: Manually log vitals
- **Controller**: `PatientProfileController.cs`
- **Action**: `LogVitals(GET)` → Vitals form
- **Action**: `LogVitals(POST)` → Save vitals
- **ViewModel**: `PatientProfileViewModel` (extended for vitals)
- **Vitals**: BP, blood sugar, oxygen, temperature

### Story 4: Upload lab results with OCR
- **Controller**: `LabResultsController.cs`
- **Action**: `Upload(GET)` → Upload form
- **Action**: `Upload(POST)` → Process upload & OCR
- **ViewModel**: `LabUploadViewModel`
- **Features**: File upload, OCR analysis, lab value parsing

### Story 5: Perform risk self-assessment
- **Controller**: `RiskAssessmentController.cs`
- **Action**: `Create(GET)` → Assessment form
- **Action**: `Create(POST)` → Calculate risk
- **ViewModel**: `RiskAssessmentInputViewModel`
- **Features**: Vitals input, symptom selection

### Story 6: View risk assessment results
- **Controller**: `RiskAssessmentController.cs`
- **Action**: `Results(id, GET)` → Show results
- **ViewModel**: `RiskAssessmentResultViewModel` (new)
- **Features**: Risk level, color indicators, clinical recommendations

### Story 7: Search nearby providers & slots
- **Controller**: `NearbyProvidersController.cs`
- **Action**: `Search(GET)` → Search form
- **Action**: `Search(POST)` → Query providers
- **ViewModel**: `NearbySearchViewModel`
- **Features**: Location-based search, filter by specialty

### Story 8: Book appointment slot
- **Controller**: `NearbyProvidersController.cs`
- **Action**: `BookSlot(id, GET)` → Booking form
- **Action**: `BookSlot(POST)` → Reserve slot
- **ViewModel**: `NearbySearchViewModel` (or AppointmentBookingViewModel)
- **Features**: Slot selection, confirmation

### Story 9: View upcoming/past appointments
- **Controller**: `PatientDashboardController.cs`
- **Action**: `ViewAppointments(GET)` → Appointments list
- **ViewModel**: `PatientDashboardViewModel` (extended)
- **Features**: Upcoming vs. past, cancellation option

### Story 10: Trigger emergency request
- **Controller**: `EmergencyController.cs`
- **Action**: `RequestAmbulance(POST)` → Create emergency
- **ViewModel**: `EmergencyRequestViewModel`
- **Features**: Location coordinates, emergency type

### Story 11: Track emergency request
- **Controller**: `EmergencyController.cs`
- **Action**: `Track(GET, id)` → Live tracking
- **ViewModel**: `EmergencyTrackingViewModel`
- **Features**: Provider name, arrival time, distance

### Story 12: Invite family members
- **Controller**: `FamilyLinkingController.cs`
- **Action**: `Invite(GET)` → Invite form
- **Action**: `Invite(POST)` → Send invitation
- **ViewModel**: `FamilyInviteViewModel`
- **Features**: Email input, permission selection

### Story 13: Accept family link invitation
- **Controller**: `FamilyLinkingController.cs`
- **Action**: `AcceptInvite(token, GET)` → Confirmation page
- **Action**: `AcceptInvite(POST)` → Accept link
- **ViewModel**: `FamilyInviteViewModel` (response)
- **Features**: Token validation, permission review

### Story 14: View notifications & mark read
- **Controller**: `PatientDashboardController.cs`
- **Action**: `Notifications(GET)` → Notification list
- **Action**: `MarkNotificationRead(POST)` → Mark as read
- **ViewModel**: `PatientDashboardViewModel` (notifications)

### Story 15: Chat with doctor
- **Controller**: `ChatController.cs`
- **Action**: `Thread(GET, otherUserId)` → Conversation
- **Action**: `SendMessage(POST)` → Send message
- **ViewModel**: `ChatThreadViewModel`
- **Features**: Message history, real-time updates

### Story 16: Chat with AI chatbot
- **Controller**: `ChatbotController.cs`
- **Action**: `Index(GET)` → Chatbot UI
- **Action**: `Ask(POST)` → Submit query
- **ViewModel**: `ChatbotViewModel`
- **Features**: Medical Q&A, health tips

---

## 🏥 4.3 DOCTORS (12 Stories)

### Story 1: View dashboard & scheduling stats
- **Controller**: `DoctorDashboardController.cs`
- **Action**: `Index(GET)` → Dashboard
- **ViewModel**: `DoctorDashboardViewModel`
- **Features**: Today's agenda, completion rates, KPIs

### Story 2: Update professional profile
- **Controller**: `DoctorProfileController.cs`
- **Action**: `Edit(GET)` → Edit form
- **Action**: `Edit(POST)` → Save profile
- **ViewModel**: `DoctorProfileViewModel`
- **Fields**: Specialization, experience, bio, fee, availability status

### Story 3: List/add/delete/bulk-create slots
- **Controller**: `DoctorSlotsController.cs`
- **Action**: `Index(GET)` → Slots list
- **Action**: `Create(GET)` → Add slot form
- **Action**: `Create(POST)` → Save single slot
- **Action**: `BulkCreate(GET)` → Bulk form
- **Action**: `BulkCreate(POST)` → Save multiple slots
- **Action**: `Delete(POST, id)` → Remove slot
- **ViewModels**: `CreateAvailableSlotViewModel`, `BulkCreateSlotsViewModel`

### Story 4: View & update appointment status
- **Controller**: `DoctorAppointmentsController.cs`
- **Action**: `Index(GET)` → Appointments list
- **Action**: `UpdateStatus(POST, id)` → Change status
- **ViewModel**: `UpdateAppointmentStatusViewModel`
- **Statuses**: Scheduled, Confirmed, Completed, Cancelled, No-Show
- **Features**: Clinical notes

### Story 5: Search patient profiles
- **Controller**: `DoctorPatientsController.cs`
- **Action**: `Search(GET)` → Search form
- **Action**: `Search(POST)` → Query results
- **ViewModel**: `PatientSearchViewModel`
- **Fields**: Name, phone, email

### Story 6: View patient medical history
- **Controller**: `DoctorPatientsController.cs`
- **Action**: `Details(GET, patientId)` → Full history
- **ViewModel**: `PatientSearchResultViewModel` (extended)
- **Features**: Past records, lab uploads, risk scores

### Story 7: View AI medical summary
- **Controller**: `DoctorPatientsController.cs`
- **Action**: `AISummary(GET, patientId)` → AI summary
- **ViewModel**: `MedicalAISummaryViewModel` (new)
- **Features**: Critical findings, missing data flags

### Story 8: View deterioration probability
- **Controller**: `DoctorPatientsController.cs`
- **Action**: `TrendAnalysis(GET, patientId)` → Trend chart
- **ViewModel**: `PatientTrendViewModel` (new)
- **Features**: Deterioration probability, timeline

### Story 9: Add medical record
- **Controller**: `MedicalRecordsController.cs`
- **Action**: `Create(GET, patientId)` → Form
- **Action**: `Create(POST)` → Save record
- **ViewModel**: `MedicalRecordCreateViewModel`
- **Fields**: Diagnosis, treatment, vitals, medications

### Story 10: View Panic Inbox
- **Controller**: `DoctorPanicInboxController.cs`
- **Action**: `Index(GET)` → Urgent cases
- **ViewModel**: `DoctorPanicInboxViewModel`
- **Features**: Assigned cases, unassigned cases

### Story 11: Self-assign critical case
- **Controller**: `DoctorPanicInboxController.cs`
- **Action**: `Claim(POST, id)` → Assign to self
- **ViewModel**: `DoctorPanicInboxViewModel`
- **Features**: Case details, allocation

### Story 12: Chat with patients
- **Controller**: `ChatController.cs`
- **Action**: `Thread(GET, otherUserId)` → Conversation
- **Action**: `SendMessage(POST)` → Send message
- **ViewModel**: `ChatThreadViewModel`
- **Features**: Message history, patient context

---

## 🏨 4.4 HOSPITAL STAFF (4 Stories)

### Story 1: View emergency triage queue
- **Controller**: `HospitalQueueController.cs`
- **Action**: `Index(GET)` → Queue list
- **ViewModel**: `HospitalQueueViewModel`
- **Features**: Pending, accepted, escalated cases

### Story 2: View patient medical info & location
- **Controller**: `HospitalQueueController.cs`
- **Action**: `Details(GET, id)` → Patient details
- **ViewModel**: `HospitalEmergencyDetailViewModel`
- **Fields**: Blood type, allergies, chronic illnesses, location coordinates

### Story 3: Accept/reject emergency request
- **Controller**: `HospitalQueueController.cs`
- **Action**: `Respond(POST)` → Accept/reject decision
- **ViewModel**: `HospitalRespondViewModel`
- **Features**: Bed reservation, escalation on reject

### Story 4: Update available emergency beds
- **Controller**: `HospitalQueueController.cs`
- **Action**: `UpdateBeds(POST)` → Update capacity
- **ViewModel**: `HospitalBedsUpdateViewModel`
- **Fields**: Emergency beds available, ICU beds, isolation beds

---

## 👨‍💼 4.5 SYSTEM ADMINISTRATORS (12 Stories)

### Story 1: View system dashboard KPIs
- **Controller**: `AdminDashboardController.cs`
- **Action**: `Index(GET)` → Dashboard
- **ViewModel**: `AdminDashboardViewModel`
- **KPIs**: Active users, appointments today, crisis status, engagement rates

### Story 2: Manage user accounts (CRUD & bulk)
- **Controller**: `AdminUsersController.cs`
- **Action**: `Index(GET)` → Users list (paginated)
- **Action**: `UpdateStatus(POST)` → Activate/deactivate
- **Action**: `BulkAction(POST)` → Bulk operations
- **Action**: `Delete(POST, id)` → Delete user
- **ViewModels**: `AdminUserViewModel`, `UpdateUserStatusViewModel`
- **Features**: Role management, status toggling

### Story 3: Manage healthcare providers (CRUD)
- **Controller**: `AdminProvidersController.cs`
- **Action**: `Index(GET)` → Providers list
- **Action**: `Create(GET)` → Add form
- **Action**: `Create(POST)` → Register provider
- **Action**: `Edit(GET, id)` → Edit form
- **Action**: `Edit(POST)` → Update provider
- **Action**: `Delete(POST, id)` → Delete provider
- **ViewModel**: `CreateProviderViewModel`
- **Fields**: Location coordinates, emergency beds, contact info

### Story 4: View & audit activity logs
- **Note**: Requires BLL logging service
- **Location**: Custom audit trail in system logs
- **Status**: ⚠️ Requires implementation in BLL

### Story 5: View & export system reports
- **Controller**: `AdminReportsController.cs`
- **Action**: `Index(GET)` → Report templates
- **Action**: `SystemReport(GET)` → System report
- **Action**: `EpidemiologyReport(GET)` → Disease report
- **Action**: `OperationalReport(GET)` → Dispatch metrics
- **Action**: `Export(POST)` → Export to CSV/Excel
- **ViewModel**: `SystemConfigViewModel` (partially used)

### Story 6: Adjust system configurations
- **Controller**: `AdminReportsController.cs` or dedicated controller
- **ViewModel**: `SystemConfigViewModel`
- **Fields**: Session timeout, lockout limit, feature toggles
- **Features**: Configuration persistence

### Story 7: View Live Command Center
- **Controller**: `AdminCrisisController.cs`
- **Action**: `CommandCenter(GET)` → Live status
- **ViewModel**: `CrisisCommandCenterViewModel`
- **Features**: Active emergency counts, dispatch times, hospital engagements
- **Technology**: Requires SignalR for real-time updates

### Story 8: View crisis heatmap
- **Controller**: `AdminCrisisController.cs`
- **Action**: `Heatmap(GET)` → Geographic heatmap
- **ViewModel**: `CrisisHeatmapViewModel`
- **Features**: Critical case clusters, outbreak zones
- **Technology**: Requires mapping API (Google Maps, Mapbox)

### Story 9: CRUD crisis configurations
- **Controller**: `AdminCrisisController.cs`
- **Action**: `Index(GET)` → Crises list
- **Action**: `Create(GET)` → Add crisis form
- **Action**: `Create(POST)` → Register crisis
- **Action**: `Edit(GET, id)` → Edit form
- **Action**: `Edit(POST)` → Update crisis
- **Action**: `Details(GET, id)` → Full details
- **ViewModels**: `CreateCrisisViewModel`, `CrisisConfigViewModel`
- **Fields**: Crisis type, thresholds, start/end dates, activation status

### Story 10: CRUD symptom weights for a crisis
- **Controller**: `AdminCrisisController.cs`
- **Action**: `AddSymptom(POST)` → Add weight
- **Action**: `UpdateSymptom(POST)` → Update weight
- **Action**: `RemoveSymptom(POST)` → Delete weight
- **ViewModel**: `SymptomWeightViewModel` (new)
- **Features**: Multiplier adjustment, real-time effect

### Story 11: Activate/deactivate crisis configuration
- **Controller**: `AdminCrisisController.cs`
- **Action**: `Activate(POST, id)` → Enable crisis mode
- **Action**: `Deactivate(POST, id)` → Disable crisis mode
- **Effect**: Alters risk algorithms, changes dashboard KPIs

### Story 12: Approve/reject crisis escalations
- **Controller**: `AdminCrisisController.cs`
- **Action**: `Approve(POST, id)` → Approve escalation
- **Action**: `Reject(POST, reason)` → Reject escalation
- **Features**: Manual validation, audit trail
- **Notes**: For newly escalated outbreak zones

---

## 📊 SUMMARY TABLE

| Role | Total Stories | Controllers | Actions | ViewModels |
|------|---|---|---|---|
| Guests | 4 | 2 (Account, Home) | 6 | 5 |
| Patients | 15 | 8 | 25+ | 10+ |
| Doctors | 12 | 6 | 20+ | 8+ |
| Hospital Staff | 4 | 1 | 4 | 4 |
| Admins | 12 | 5 | 20+ | 10+ |
| **TOTAL** | **47** | **22** | **75+** | **37+** |

---

## ✅ IMPLEMENTATION CHECKLIST

- [x] All Controllers created with TODO comments
- [x] All primary ViewModels created
- [x] Authorization attributes applied per role
- [x] Build verified successfully
- [ ] Implement TODO actions (BLL service calls)
- [ ] Create Views (.cshtml files)
- [ ] Wire Dependency Injection in Program.cs
- [ ] Create missing service interfaces
- [ ] Add SignalR support (real-time features)
- [ ] Implement validation attributes
- [ ] Add unit & integration tests
- [ ] Security audit & testing

---

**Document Version**: 1.0  
**Last Updated**: 2025  
**Project**: Etmen Health Emergency Management System

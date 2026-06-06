using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Doctor;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Doctor Patients Controller
    /// Search patient registry, review histories, record diagnostic logs, and show AI summaries
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorPatientsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly ICriticalIntelligenceService _criticalIntelligenceService;
        private readonly ILogger<DoctorPatientsController> _logger;

        public DoctorPatientsController(
            IDoctorService doctorService,
            IPatientService patientService,
            ICriticalIntelligenceService criticalIntelligenceService,
            ILogger<DoctorPatientsController> logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _criticalIntelligenceService = criticalIntelligenceService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /DoctorPatients/Search
        /// Displays patient search lookup page
        /// </summary>
        [HttpGet]
        public IActionResult Search()
        {
            var viewModel = new PatientSearchViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// POST: /DoctorPatients/Search
        /// Returns patients matching keyword (AJAX)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return PartialView("_PatientList", new List<Etmen_BLL.DTOs.Doctor.PatientSearchDto>());

            try
            {
                var searchResult = await _doctorService.SearchPatientsAsync(searchTerm);

                if (!searchResult.IsSuccess)
                {
                    _logger.LogWarning("Search failed for term: {SearchTerm}", searchTerm);
                    return PartialView("_PatientList", new List<Etmen_BLL.DTOs.Doctor.PatientSearchDto>());
                }

                return PartialView("_PatientList", searchResult.Data ?? new List<Etmen_BLL.DTOs.Doctor.PatientSearchDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching patients with term {SearchTerm}", searchTerm);
                return PartialView("_PatientList", new List<Etmen_BLL.DTOs.Doctor.PatientSearchDto>());
            }
        }

        /// <summary>
        /// GET: /DoctorPatients/Details
        /// Renders patient clinical records, AI summary, and deterioration warnings
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int patientProfileId)
        {
            try
            {
                if (patientProfileId <= 0)
                {
                    TempData["Error"] = "معرف مريض غير صالح";
                    return RedirectToAction(nameof(Search));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Doctor {UserId} accessing patient {PatientId} details", userId, patientProfileId);

                // Get patient profile
                var profileResult = await _patientService.GetProfileAsync(patientProfileId.ToString());
                if (!profileResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch patient profile {PatientId}", patientProfileId);
                    TempData["Error"] = "Failed to load patient data";
                    return RedirectToAction(nameof(Search));
                }

                // Get AI medical summary
                var summaryResult = await _criticalIntelligenceService.GenerateMedicalSummaryAsync(patientProfileId);
                var summary = summaryResult.IsSuccess ? summaryResult.Data : null;

                // Get deterioration prediction
                var deteriorationResult = await _criticalIntelligenceService.PredictDeteriorationAsync(patientProfileId);
                var deterioration = deteriorationResult.IsSuccess ? deteriorationResult.Data : null;

                var viewModel = new Etmen_PL.Models.ViewModels.Patient.PatientDashboardViewModel
                {
                    PatientName = profileResult.Data?.FullName ?? "",
                    RecentAlerts = new List<Etmen_BLL.DTOs.Patient.RecentAlertDto>(),
                    UpcomingAppointments = new List<Etmen_BLL.DTOs.Patient.RecentAppointmentDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient details for {PatientProfileId}", patientProfileId);
                TempData["Error"] = "Error loading patient data";
                return RedirectToAction(nameof(Search));
            }
        }

        /// <summary>
        /// GET: /DoctorPatients/AddMedicalRecord
        /// Show form to add medical record for patient
        /// </summary>
        [HttpGet]
        public IActionResult AddMedicalRecord(int patientProfileId)
        {
            try
            {
                if (patientProfileId <= 0)
                {
                    TempData["Error"] = "Invalid patient ID";
                    return RedirectToAction(nameof(Search));
                }

                var viewModel = new MedicalRecordCreateViewModel
                {
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading add medical record form");
                TempData["Error"] = "Error loading form";
                return RedirectToAction(nameof(Search));
            }
        }

        /// <summary>
        /// POST: /DoctorPatients/AddMedicalRecord
        /// Documents diagnosis and treatment notes for a patient
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMedicalRecord(int patientProfileId, MedicalRecordCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                if (patientProfileId <= 0)
                {
                    TempData["Error"] = "Invalid patient ID";
                    return RedirectToAction(nameof(Search));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var recordDto = new Etmen_BLL.DTOs.Medical.MedicalRecordCreateDto
                {
                    PatientId = patientProfileId,
                    RecordDate = DateTime.UtcNow,
                    SystolicBP = viewModel.SystolicBP,
                    DiastolicBP = viewModel.DiastolicBP,
                    BloodSugar = viewModel.BloodSugar,
                    HeartRate = viewModel.HeartRate,
                    Temperature = viewModel.Temperature,
                    OxygenSaturation = viewModel.OxygenSaturation,
                    Symptoms = viewModel.Symptoms,
                    Notes = viewModel.Notes
                };

                var result = await _doctorService.AddMedicalRecordForPatientAsync(userId, recordDto);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to add medical record for patient {PatientId}", patientProfileId);
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error);
                    return View(viewModel);
                }

                _logger.LogInformation("Medical record added for patient {PatientId} by doctor {UserId}", 
                    patientProfileId, userId);
                TempData["Success"] = "Medical record added successfully";
                return RedirectToAction(nameof(Details), new { patientProfileId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding medical record for patient {PatientId}", patientProfileId);
                ModelState.AddModelError(string.Empty, "Error adding medical record");
                return View(viewModel);
            }
        }
    }
}

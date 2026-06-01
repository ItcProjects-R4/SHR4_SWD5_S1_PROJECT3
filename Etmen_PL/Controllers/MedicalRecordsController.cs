using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Medical Records Controller
    /// Renders vitals logs and logs manual entries
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class MedicalRecordsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<MedicalRecordsController> _logger;

        public MedicalRecordsController(
            IPatientService patientService,
            ILogger<MedicalRecordsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /MedicalRecords/Index
        /// Lists previous medical logs
        /// TODO: Get current user ID
        /// TODO: Call _patientService.GetMedicalRecordsAsync(userId)
        /// TODO: Return View with IEnumerable<MedicalRecordDto>
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Implementation
                var records = new List<object>();
                return View(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving medical records");
                TempData["Error"] = "خطأ في تحميل السجلات الطبية";
                return RedirectToAction("Index", "PatientDashboard");
            }
        }

        /// <summary>
        /// POST: /MedicalRecords/Create
        /// Logs a manual patient vitals entry
        /// TODO: Validate ModelState
        /// TODO: Get current user ID
        /// TODO: Map MedicalRecordCreateViewModel to MedicalRecordCreateDto
        /// TODO: Call _patientService.AddMedicalRecordAsync(userId, dto)
        /// TODO: Redirect to Index on success
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalRecordCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            try
            {
                // TODO: Implementation
                _logger.LogInformation("New medical record created");
                TempData["Success"] = "تم تسجيل السجل الطبي بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating medical record");
                TempData["Error"] = "خطأ في إنشاء السجل الطبي";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Admin
{
    public class SystemConfigViewModel
    {
        public bool EnableCrisisMode { get; set; }
        public bool EnableAIChat { get; set; }
        public bool EnableOCR { get; set; }
        public bool EnableFamilyLinking { get; set; }
        public bool EnableEmergencyRequests { get; set; }

        [Range(1, 10)]
        public int MaxLoginAttempts { get; set; } = 5;

        [Range(1, 120)]
        public int LockoutDurationMinutes { get; set; } = 15;
    }
}

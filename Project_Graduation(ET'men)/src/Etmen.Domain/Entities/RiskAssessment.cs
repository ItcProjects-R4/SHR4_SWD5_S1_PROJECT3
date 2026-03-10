using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// AI-generated risk evaluation for a single MedicalRecord.
/// Stores the probability score, level classification, and actionable recommendations.
/// </summary>
public class RiskAssessment : BaseEntity
{
    public Guid         RecordId                      { get; private set; }
    public Guid         PatientId                     { get; private set; }
    public double       RiskScore                     { get; private set; } // 0.0 – 1.0
    public RiskLevel    RiskLevel                     { get; private set; }
    public string       ModelVersion                  { get; private set; } = default!;
    public List<string> Recommendations               { get; private set; } = new();
    public List<string> RequiredAnalyses              { get; private set; } = new();
    public List<string> RecommendedDoctorSpecialties  { get; private set; } = new();
    public string?      PrimaryIssue                  { get; private set; }

    private RiskAssessment() { }

    public static RiskAssessment Create(Guid recordId, Guid patientId, double score,
                                        RiskLevel level, string modelVersion,
                                        List<string> recommendations,
                                        List<string> requiredAnalyses,
                                        List<string> doctorSpecialties)
    {
        throw new NotImplementedException();
    }

    /// <summary>Returns true when the risk level requires immediate escalation.</summary>
    public bool IsHighRisk() => RiskLevel == RiskLevel.High;
}

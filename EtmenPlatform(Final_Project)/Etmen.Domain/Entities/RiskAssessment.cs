using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// AI-generated risk evaluation linked to a MedicalRecord.
/// Stores the probability score, classified level, and actionable outputs.
/// </summary>
public class RiskAssessment : BaseEntity
{
    public Guid       RecordId                         { get; private set; }
    public Guid       PatientId                        { get; private set; }
    public double     RiskScore                        { get; private set; } // 0.0 – 1.0
    public RiskLevel  RiskLevel                        { get; private set; }
    public string     ModelVersion                     { get; private set; } = string.Empty;
    public string?    PrimaryIssue                     { get; private set; }
    public List<string> Recommendations                { get; private set; } = new();
    public List<string> RequiredAnalyses               { get; private set; } = new();
    public List<string> RecommendedDoctorSpecialties   { get; private set; } = new();

    protected RiskAssessment() { }

    public static RiskAssessment Create(Guid recordId, Guid patientId, double score,
        RiskLevel level, string modelVersion, string? primaryIssue,
        List<string> recommendations, List<string> requiredAnalyses,
        List<string> recommendedSpecialties)
        => new()
        {
            RecordId = recordId, PatientId = patientId,
            RiskScore = score, RiskLevel = level,
            ModelVersion = modelVersion, PrimaryIssue = primaryIssue,
            Recommendations = recommendations,
            RequiredAnalyses = requiredAnalyses,
            RecommendedDoctorSpecialties = recommendedSpecialties
        };

    public bool IsHighRisk() => RiskLevel == RiskLevel.High;
}

using Etmen.Domain.Enums;

namespace Etmen.Application.Services;

/// <summary>
/// Abstracts the ML model inference pipeline.
/// The concrete implementation (MLModelService) lives in Infrastructure/AI.
/// </summary>
public interface IRiskCalculationService
{
    Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features, CancellationToken ct = default);
    RiskLevel ClassifyRisk(double probabilityScore);
    List<RequiredAnalysis>  GetRequiredAnalyses(RiskLevel level, RiskFeatures features);
    List<RecommendedDoctor> GetRecommendedDoctors(RiskLevel level);
    List<string>            GetRecommendations(RiskLevel level, RiskFeatures features);
}

// ── Supporting value types ──────────────────────────────────────────────────

public sealed record RiskFeatures(
    float Age, float BMI, float BloodPressureSystolic,
    float BloodPressureDiastolic, float BloodSugar,
    float FamilyHistory, float IsSmoker, float ActivityLevel);

public sealed record RiskPredictionResult(double Probability, bool IsHighRisk);

public sealed record RequiredAnalysis(
    string Name, string Description, AnalysisPriority Priority, string Icon);

public sealed record RecommendedDoctor(
    string Name, string Specialty, double Rating, List<string> Slots);

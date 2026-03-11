using Etmen.Application.DTOs.Internal;
using Etmen.Domain.Enums;

namespace Etmen.Application.Services;

/// <summary>Contract for the AI risk scoring engine (ML.NET or Python bridge).</summary>
public interface IRiskCalculationService
{
    Task<RiskPredictionResult>    CalculateRiskAsync(RiskFeatures features, CancellationToken ct = default);
    RiskLevel                     ClassifyRisk(double probabilityScore);
    List<RequiredAnalysis>        GetRequiredAnalyses(RiskLevel level, RiskFeatures features);
    List<RecommendedDoctor>       GetRecommendedDoctors(RiskLevel level);
    List<string>                  GetRecommendations(RiskLevel level, RiskFeatures features);
}

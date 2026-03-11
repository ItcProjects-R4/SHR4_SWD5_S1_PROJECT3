using Microsoft.Extensions.Configuration;
using Etmen.Application.DTOs.Internal;
using Etmen.Application.Services;
using Etmen.Domain.Enums;

namespace Etmen.Infrastructure.AI;

/// <summary>
/// Implements IRiskCalculationService using ML.NET (ONNX/ZIP model).
/// Model is loaded once at startup and hot-reloaded on file change.
/// Replace placeholder with actual ML.NET PredictionEnginePool usage.
/// </summary>
public sealed class MLModelService : IRiskCalculationService
{
    public Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features, CancellationToken ct = default)
        => throw new NotImplementedException();

    public RiskLevel ClassifyRisk(double probabilityScore)
        => throw new NotImplementedException();

    public List<RequiredAnalysis> GetRequiredAnalyses(RiskLevel level, RiskFeatures features)
        => throw new NotImplementedException();

    public List<RecommendedDoctor> GetRecommendedDoctors(RiskLevel level)
        => throw new NotImplementedException();

    public List<string> GetRecommendations(RiskLevel level, RiskFeatures features)
        => throw new NotImplementedException();
}

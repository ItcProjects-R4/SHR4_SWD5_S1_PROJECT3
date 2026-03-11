using Etmen.Application.DTOs.Internal;

namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the RiskAssessmentResponse operation.</summary>
public sealed class RiskAssessmentResponse
{
    public Guid              AssessmentId           { get; set; }
    public double            Score                  { get; set; }
    public string            RiskLevel              { get; set; } = string.Empty;
    public string?           PrimaryIssue           { get; set; }
    public List<RequiredAnalysis>  RequiredAnalyses  { get; set; } = new();
    public List<RecommendedDoctor> RecommendedDoctors { get; set; } = new();
    public List<string>      Recommendations        { get; set; } = new();
    public DateTime          AssessedAt             { get; set; }
}

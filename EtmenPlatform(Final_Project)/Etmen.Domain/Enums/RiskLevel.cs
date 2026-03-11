namespace Etmen.Domain.Enums;

/// <summary>Classified risk level from the AI model probability score.</summary>
public enum RiskLevel
{
    Low    = 0,   // score < 0.3
    Medium = 1,   // score 0.3 - 0.6
    High   = 2    // score >= 0.6
}

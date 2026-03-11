namespace Etmen.Domain.Enums;

/// <summary>Priority of a required lab analysis returned by the risk engine.</summary>
public enum AnalysisPriority
{
    Urgent        = 0,
    WithinWeek    = 1,
    WithinTwoWeeks = 2
}

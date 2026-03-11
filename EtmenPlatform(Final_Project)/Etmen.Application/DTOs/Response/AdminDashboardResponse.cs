namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the AdminDashboardResponse operation.</summary>
public sealed class AdminDashboardResponse
{
    public int TotalPatients   { get; set; }
    public int HighRiskCount   { get; set; }
    public int MediumRiskCount { get; set; }
    public int LowRiskCount    { get; set; }
    public int UrgentAlerts    { get; set; }
}

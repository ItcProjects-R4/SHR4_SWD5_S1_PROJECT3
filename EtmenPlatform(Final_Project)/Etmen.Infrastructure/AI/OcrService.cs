using Etmen.Application.DTOs.Response;
using Etmen.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Etmen.Infrastructure.AI;

/// <summary>
/// Implements IOcrService.
/// Configure via appsettings: Ocr:Provider ("Tesseract" or "AzureComputerVision").
/// Extracts: BloodSugar, HbA1c, Cholesterol, HDL, LDL, Triglycerides, Creatinine, Urea, LabName, LabDate.
/// </summary>
public sealed class OcrService : IOcrService
{
    private readonly IConfiguration _config;

    public OcrService(IConfiguration config) => _config = config;

    public Task<ExtractedLabValues> ExtractLabValuesAsync(IFormFile file, CancellationToken ct = default)
        => throw new NotImplementedException();
}

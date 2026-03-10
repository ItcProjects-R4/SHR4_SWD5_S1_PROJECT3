using Microsoft.AspNetCore.Http;

namespace Etmen.Application.Services;

/// <summary>
/// Contract for OCR extraction of lab values from uploaded PDF or image files.
/// Implemented by OcrService in Infrastructure/AI using Tesseract.NET or Azure CV.
/// </summary>
public interface IOcrService
{
    /// <summary>Accepts a PDF or image file and returns extracted numeric lab values.</summary>
    Task<ExtractedLabValues> ExtractLabValuesAsync(IFormFile file, CancellationToken ct = default);
}

public sealed record ExtractedLabValues(
    double?  BloodSugar,
    double?  HbA1c,
    double?  Cholesterol,
    double?  HDL,
    double?  LDL,
    double?  Triglycerides,
    double?  Creatinine,
    double?  Urea,
    string   LabName,
    DateTime? LabDate);

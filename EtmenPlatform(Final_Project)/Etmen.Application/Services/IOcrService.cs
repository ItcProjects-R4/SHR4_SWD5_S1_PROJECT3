using Etmen.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;

namespace Etmen.Application.Services;

/// <summary>
/// Contract for OCR lab result extraction.
/// Implemented by OcrService using Tesseract.NET or Azure Computer Vision.
/// </summary>
public interface IOcrService
{
    Task<ExtractedLabValues> ExtractLabValuesAsync(IFormFile file, CancellationToken ct = default);
}

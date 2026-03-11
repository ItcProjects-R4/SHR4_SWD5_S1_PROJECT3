using Microsoft.AspNetCore.Http;

namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the UploadLabResultRequest operation.</summary>
public sealed class UploadLabResultRequest
{
    public Guid      PatientId { get; set; }
    public IFormFile File      { get; set; } = null!;
}

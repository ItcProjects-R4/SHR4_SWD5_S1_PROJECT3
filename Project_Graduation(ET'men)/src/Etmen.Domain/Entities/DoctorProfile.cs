using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Profile for a Doctor user — stores specialty and license details.
/// </summary>
public class DoctorProfile : BaseEntity
{
    public Guid   UserId      { get; private set; }
    public string Specialty   { get; private set; } = default!;
    public string LicenseNo   { get; private set; } = default!;

    private DoctorProfile() { }

    public static DoctorProfile Create(Guid userId, string specialty, string licenseNo)
    {
        throw new NotImplementedException();
    }
}

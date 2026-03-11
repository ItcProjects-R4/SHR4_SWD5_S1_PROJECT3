using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>Stores professional information for a Doctor user.</summary>
public class DoctorProfile : BaseEntity
{
    public Guid   UserId               { get; private set; }
    public string Specialty            { get; private set; } = string.Empty;
    public string LicenseNumber        { get; private set; } = string.Empty;
    public string HospitalAffiliation  { get; private set; } = string.Empty;

    protected DoctorProfile() { }

    public static DoctorProfile Create(Guid userId, string specialty,
        string licenseNumber, string hospitalAffiliation)
        => new()
        {
            UserId = userId, Specialty = specialty,
            LicenseNumber = licenseNumber, HospitalAffiliation = hospitalAffiliation
        };
}

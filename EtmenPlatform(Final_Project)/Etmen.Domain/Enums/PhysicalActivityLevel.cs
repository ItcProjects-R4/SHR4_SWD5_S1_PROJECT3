namespace Etmen.Domain.Enums;

/// <summary>Patient physical activity level — used as AI model feature input (0–4).</summary>
public enum PhysicalActivityLevel
{
    Sedentary  = 0,
    Light      = 1,
    Moderate   = 2,
    Active     = 3,
    VeryActive = 4
}

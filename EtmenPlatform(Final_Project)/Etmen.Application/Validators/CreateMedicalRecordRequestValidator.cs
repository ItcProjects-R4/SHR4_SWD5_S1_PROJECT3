using Etmen.Application.DTOs.Request;
using FluentValidation;

namespace Etmen.Application.Validators;

/// <summary>
/// Validates CreateMedicalRecordRequest.
/// Enforces medically plausible ranges for all vitals.
/// </summary>
public sealed class CreateMedicalRecordRequestValidator : AbstractValidator<CreateMedicalRecordRequest>
{
    public CreateMedicalRecordRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("معرف المريض مطلوب.");

        RuleFor(x => x.BloodPressureSystolic)
            .InclusiveBetween(60, 300)
            .WithMessage("ضغط الدم الانقباضي يجب أن يكون بين 60 و 300 mmHg.");

        RuleFor(x => x.BloodPressureDiastolic)
            .InclusiveBetween(40, 200)
            .WithMessage("ضغط الدم الانبساطي يجب أن يكون بين 40 و 200 mmHg.");

        RuleFor(x => x.BloodSugar)
            .InclusiveBetween(20, 600)
            .WithMessage("سكر الدم يجب أن يكون بين 20 و 600 mg/dL.");

        RuleFor(x => x.BMI)
            .InclusiveBetween(10, 80)
            .WithMessage("مؤشر كتلة الجسم يجب أن يكون بين 10 و 80.");

        RuleFor(x => x.ActivityLevel)
            .InclusiveBetween(0, 4)
            .WithMessage("مستوى النشاط يجب أن يكون بين 0 و 4.");

        RuleFor(x => x.Symptoms)
            .MaximumLength(1000)
            .WithMessage("الأعراض يجب ألا تتجاوز 1000 حرف.")
            .When(x => x.Symptoms is not null);
    }
}

using Etmen.Application.DTOs.Request;
using FluentValidation;

namespace Etmen.Application.Validators;

/// <summary>Validates UpdatePatientProfileRequest.</summary>
public sealed class UpdatePatientProfileRequestValidator : AbstractValidator<UpdatePatientProfileRequest>
{
    public UpdatePatientProfileRequestValidator()
    {
        RuleFor(x => x.Height)
            .InclusiveBetween(50, 280)
            .WithMessage("الطول يجب أن يكون بين 50 و 280 سم.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(2, 500)
            .WithMessage("الوزن يجب أن يكون بين 2 و 500 كجم.");

        RuleFor(x => x.ActivityLevel)
            .InclusiveBetween(0, 4)
            .WithMessage("مستوى النشاط يجب أن يكون بين 0 و 4.");
    }
}

/// <summary>Validates AIChatRequest.</summary>
public sealed class AIChatRequestValidator : AbstractValidator<AIChatRequest>
{
    public AIChatRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("معرف المريض مطلوب.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("الرسالة مطلوبة.")
            .MaximumLength(2000).WithMessage("الرسالة يجب ألا تتجاوز 2000 حرف.");
    }
}

/// <summary>Validates UploadLabResultRequest.</summary>
public sealed class UploadLabResultRequestValidator : AbstractValidator<UploadLabResultRequest>
{
    private static readonly string[] AllowedTypes = ["application/pdf", "image/jpeg", "image/png"];

    public UploadLabResultRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("معرف المريض مطلوب.");

        RuleFor(x => x.File)
            .NotNull().WithMessage("الملف مطلوب.")
            .Must(f => f.Length <= 10 * 1024 * 1024)
            .WithMessage("حجم الملف يجب ألا يتجاوز 10 ميجابايت.")
            .Must(f => AllowedTypes.Contains(f.ContentType))
            .WithMessage("نوع الملف يجب أن يكون PDF أو JPEG أو PNG.")
            .When(x => x.File is not null);
    }
}

/// <summary>Validates BookAppointmentRequest.</summary>
public sealed class BookAppointmentRequestValidator : AbstractValidator<BookAppointmentRequest>
{
    public BookAppointmentRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("معرف المريض مطلوب.");

        RuleFor(x => x.ProviderId)
            .NotEmpty().WithMessage("معرف مقدم الرعاية مطلوب.");

        RuleFor(x => x.SlotId)
            .NotEmpty().WithMessage("معرف الموعد مطلوب.");
    }
}

/// <summary>Validates InviteMemberRequest.</summary>
public sealed class InviteMemberRequestValidator : AbstractValidator<InviteMemberRequest>
{
    public InviteMemberRequestValidator()
    {
        RuleFor(x => x.PrimaryUserId)
            .NotEmpty().WithMessage("معرف المستخدم الرئيسي مطلوب.");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress()
            .WithMessage("البريد الإلكتروني للمدعو غير صحيح.");

        RuleFor(x => x.Relationship)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("العلاقة مطلوبة (Parent, Child, Spouse, إلخ).");
    }
}

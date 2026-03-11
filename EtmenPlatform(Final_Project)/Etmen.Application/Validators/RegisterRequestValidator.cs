using Etmen.Application.DTOs.Request;
using FluentValidation;

namespace Etmen.Application.Validators;

/// <summary>
/// Validates RegisterRequest before it reaches the handler.
/// Enforces email format, password strength, and required fields.
/// </summary>
public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("الاسم الكامل مطلوب.")
            .MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
            .EmailAddress().WithMessage("البريد الإلكتروني غير صحيح.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("كلمة المرور مطلوبة.")
            .MinimumLength(8).WithMessage("كلمة المرور يجب أن تكون 8 أحرف على الأقل.")
            .Matches("[A-Z]").WithMessage("يجب أن تحتوي على حرف كبير.")
            .Matches("[0-9]").WithMessage("يجب أن تحتوي على رقم.");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(r => r is "Patient" or "Doctor")
            .WithMessage("الدور يجب أن يكون Patient أو Doctor.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow.AddYears(-10))
            .WithMessage("تاريخ الميلاد غير صحيح.")
            .GreaterThan(DateTime.UtcNow.AddYears(-120))
            .WithMessage("تاريخ الميلاد غير صحيح.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(g => g is "Male" or "Female" or "ذكر" or "أنثى")
            .WithMessage("الجنس يجب أن يكون Male أو Female.");
    }
}

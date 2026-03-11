using Etmen.Application.DTOs.Request;
using FluentValidation;

namespace Etmen.Application.Validators;

/// <summary>Validates LoginRequest — email format and password presence.</summary>
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
            .EmailAddress().WithMessage("البريد الإلكتروني غير صحيح.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("كلمة المرور مطلوبة.");
    }
}

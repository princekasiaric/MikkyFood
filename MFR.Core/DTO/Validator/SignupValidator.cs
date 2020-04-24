using FluentValidation;
using MFR.Core.DTO.Request;

namespace MFR.Core.DTO.Validator
{
    public class SignupValidator : AbstractValidator<SignupRequest>
    {
        public SignupValidator()
        {
            RuleFor(s => s.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(s => s.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(s => s.ConfirmPassword).Equal(s => s.Password).When(s => !string.IsNullOrWhiteSpace(s.Password))
                                                      .WithMessage("Passwords should match");
        }
    }
}

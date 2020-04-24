using FluentValidation;
using MFR.Core.DTO.Request;

namespace MFR.Core.DTO.Validator
{
    public class SigninValidator : AbstractValidator<SigninRequest>
    {
        public SigninValidator()
        {
            RuleFor(s => s.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(s => s.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}

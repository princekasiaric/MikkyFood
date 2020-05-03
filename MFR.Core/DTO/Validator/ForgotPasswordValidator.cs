using FluentValidation;
using MFR.Core.DTO.Request;

namespace MFR.Core.DTO.Validator
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(fp => fp.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        }
    }
}

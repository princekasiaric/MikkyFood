using FluentValidation;
using MFR.DomainModels;

namespace MFR.Core.DTO.Validator
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        public MenuValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Please specify name");
        }
    }
}

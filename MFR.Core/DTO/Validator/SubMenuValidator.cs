using FluentValidation;
using MFR.DomainModels;

namespace MFR.Core.DTO.Validator
{
    public class SubMenuValidator : AbstractValidator<SubMenu>
    {
        public SubMenuValidator()
        {
            RuleFor(sm => sm.Name).NotEmpty().WithMessage("Please specify name");
            RuleFor(sm => sm.Price).GreaterThan(0).WithMessage("Please specify price");
            RuleFor(sm => sm.Description).NotEmpty().WithMessage("Please specify description");
        }
    }
}

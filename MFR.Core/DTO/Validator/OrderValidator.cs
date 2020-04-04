using FluentValidation;
using MFR.DomainModels;

namespace MFR.Core.DTO.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty().WithMessage("Please specify first name");
            RuleFor(o => o.LastName).NotEmpty().WithMessage("Please specify last name");
            RuleFor(o => o.Email).EmailAddress().WithMessage("Please specify a valid email");
            RuleFor(o => o.PhoneNumber).Length(9, 13).WithMessage("Please specify a valid phone number");
            RuleFor(o => o.Address).Length(100).WithMessage("Not more than 100 characters is allowed");
            RuleFor(o => o.Location).Length(25).WithMessage("Not more than 25 characters is allowed");

            RuleFor(o => o.Reservation.Time).NotEmpty().When(o => o.Reservation != null).WithMessage("Please specify time");
            RuleFor(o => o.Reservation.Date).NotEmpty().When(o => o.Reservation != null).WithMessage("Please specify date");
            RuleFor(o => o.Reservation.NumberOfPeople).NotEmpty().When(o => o.Reservation != null)
                                                      .WithMessage("Please specify number of people");
        }
    }
}

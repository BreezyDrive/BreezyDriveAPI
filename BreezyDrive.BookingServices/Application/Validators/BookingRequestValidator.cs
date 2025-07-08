using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Interfaces;
using FluentValidation;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Validators;

public class BookingRequestValidator : AbstractValidator<BookingRequest>
{
    public BookingRequestValidator(IExistenceCheckerService existenceCheckerService)
    {
        // RuleFor(x => x.RentUserId)
        //     .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.")
        //     .Must(existenceCheckerService.IsUserExists).WithMessage("User không tồn tại.");
        RuleFor(x => x.CarId)
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(existenceCheckerService.IsCarExists).WithMessage("Car không tồn tại.");
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
    
}
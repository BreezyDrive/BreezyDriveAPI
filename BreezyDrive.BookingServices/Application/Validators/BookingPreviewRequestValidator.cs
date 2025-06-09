using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Interfaces;
using FluentValidation;

namespace BreezyDrive.BookingServices.Application.Validators;

public class BookingPreviewRequestValidator :  AbstractValidator<BookingPreviewRequest>
{
    public BookingPreviewRequestValidator(IExistenceCheckerService existenceCheckerService) 
    {
        RuleFor(x => x.CarId)
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(existenceCheckerService.IsCarExists).WithMessage("Car không tồn tại.");
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate);
        
    }
}
using BreezyDrive.BookingServices.Application.Dto.Requests;
using FluentValidation;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Validators;

public class BookingRequestValidator : AbstractValidator<BookingRequest>
{
    private readonly IRequestClient<CheckUserExistRequestEvent> _userExistsClient;
    private readonly IRequestClient<CheckCarExistRequestEvent> _carExistsClient;


    public BookingRequestValidator(IRequestClient<CheckUserExistRequestEvent> userExistsClient,
        IRequestClient<CheckCarExistRequestEvent> carExistsClient)
    {
        _userExistsClient = userExistsClient;
        _carExistsClient = carExistsClient;

        RuleFor(x => x.RentUserId)
            .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.")
            .Must(UserExists).WithMessage("User không tồn tại.");
        RuleFor(x => x.CarId)
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(CarExists).WithMessage("Car không tồn tại.");
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate);
    }

    private bool UserExists(Guid userId)
    {
        var response =
            _userExistsClient.GetResponse<CheckUserExistResponse>(
                new CheckUserExistRequestEvent { UserId = userId });
        return response.Result.Message.IsUserExists;
    }

    private bool CarExists(Guid carId)
    {
        var response =
            _carExistsClient.GetResponse<CheckCarExistResponseEvent>(new CheckCarExistRequestEvent()
                { CarId = carId });
        return response.Result.Message.IsCarExists;
    }
}
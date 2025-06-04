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
    private readonly IRequestClient<CheckUserExistRequest> _userExistsClient;
    private readonly IRequestClient<CheckCarExistRequestEvent> _carExistsClient;


    public BookingRequestValidator(IRequestClient<CheckUserExistRequest> userExistsClient,
        IRequestClient<CheckCarExistRequestEvent> carExistsClient)
    {
        _userExistsClient = userExistsClient;
        _carExistsClient = carExistsClient;

        RuleFor(x => x.RentUserId)
            .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.")
            .Must(UserExists).WithMessage("User not found");
          RuleFor(x => x.CarId)
              .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.");
           // .Must(CarExists).WithMessage("Car not found");
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate);
    }

    private bool UserExists(Guid rentUserId)
    {
        var response =
            _userExistsClient.GetResponse<CheckUserExistResponse>(new CheckUserExistRequest { UserId = rentUserId });
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
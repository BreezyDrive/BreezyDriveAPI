using BreezyDrive.BookingServices.Application.Interfaces;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Services;

public class ExistenceCheckerService : IExistenceCheckerService
{
    private readonly IRequestClient<CheckUserExistRequestEvent> _userExistsClient;
    private readonly IRequestClient<CheckCarExistRequestEvent> _carExistsClient;

    public ExistenceCheckerService(
        IRequestClient<CheckUserExistRequestEvent> userExistsClient,
        IRequestClient<CheckCarExistRequestEvent> carExistsClient)
    {
        _userExistsClient = userExistsClient;
        _carExistsClient = carExistsClient;
    }


    public bool IsUserExists(Guid userId)
    {
        var response =
            _userExistsClient.GetResponse<CheckUserExistResponse>(
                new CheckUserExistRequestEvent { UserId = userId });
        return response.Result.Message.IsUserExists;
    }

    public bool IsCarExists(Guid carId)
    {
        var response =
            _carExistsClient.GetResponse<CheckCarExistResponseEvent>(new CheckCarExistRequestEvent()
                { CarId = carId });
        return response.Result.Message.IsCarExists;
    }
}
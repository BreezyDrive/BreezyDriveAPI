using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;

namespace BreezyDrive.CarServices.Application.Messaging;

public class GetCarInformationHandler(
    ICarService carServices,
    ILogger<GetCarInformationHandler> logger)
    : IMessageHandler<GetCarInformationRequestEvent, GetCarInformationResponseEvent>
{
    public async Task<GetCarInformationResponseEvent> HandleMessageAsync(GetCarInformationRequestEvent message)
    {
        logger.LogInformation("Get information for {CarId}",  message.CarId);
        var car = await carServices.GetByGuidAsync(message.CarId);

        if (car == null)
        {
            logger.LogWarning("Car {CarId} not found", message.CarId);
        }

        return new GetCarInformationResponseEvent
        {
            CarId = car.Id,
            UserId = car.UserId,
            PricePerDay = car.PricePerDay
        };
    }
}
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingPermissionChecker(IRequestClient<GetCarInformationRequestEvent> carInfoClient)
    : IBookingPermissionChecker
{
    public async Task EnsureUserIsCarOwnerAsync(Guid carId, Guid currentUserId)
    {
        var carResponse = await carInfoClient.GetResponse<GetCarInformationResponseEvent>(
            new GetCarInformationRequestEvent { CarId = carId });

        var ownerId = carResponse.Message.UserId;

        if (ownerId != currentUserId)
            throw new CustomExceptions.UnAuthorizedException("Bạn không phải là chủ xe của đơn này.");
    }
}
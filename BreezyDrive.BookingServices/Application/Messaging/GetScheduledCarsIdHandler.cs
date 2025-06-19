using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events.BookingEvents.Requests;
using Library.EventContracts.Events.BookingEvents.Responses;

namespace BreezyDrive.BookingServices.Application.Messaging;

public class GetScheduledCarsIdHandler(
    IBookingScheduleService bookingScheduleService
) : IMessageHandler<GetScheduledCarsIdRequest, GetScheduledCarsIdResponse>
{
    public async Task<GetScheduledCarsIdResponse> HandleMessageAsync(GetScheduledCarsIdRequest message)
    {
        var cars = await bookingScheduleService.GetCarIdsAlreadyScheduled(message.StartDate, message.EndDate);

        return new GetScheduledCarsIdResponse
        {
            CarIds = cars
        };
    }
}
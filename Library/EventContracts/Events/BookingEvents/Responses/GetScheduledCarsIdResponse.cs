namespace Library.EventContracts.Events.BookingEvents.Responses;

public class GetScheduledCarsIdResponse
{
    public required IEnumerable<Guid> CarIds;

}
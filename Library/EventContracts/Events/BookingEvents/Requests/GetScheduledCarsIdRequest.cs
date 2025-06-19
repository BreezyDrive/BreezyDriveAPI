namespace Library.EventContracts.Events.BookingEvents.Requests;

public class GetScheduledCarsIdRequest
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
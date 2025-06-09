namespace BreezyDrive.BookingServices.Application.Dto.Requests;

public class BookingPreviewRequest
{
    public Guid CarId { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
}
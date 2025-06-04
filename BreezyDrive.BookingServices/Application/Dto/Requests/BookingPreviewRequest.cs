namespace BreezyDrive.BookingServices.Application.Dto.Requests;

public class BookingPreviewRequest
{
    public Guid CarId { get; set; }
    
    public string? Location { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
}
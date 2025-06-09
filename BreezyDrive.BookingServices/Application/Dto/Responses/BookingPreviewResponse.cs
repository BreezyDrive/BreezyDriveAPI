namespace BreezyDrive.BookingServices.Application.Dto.Responses;

public class BookingPreviewResponse
{
    public Guid CarId { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public int TotalDays => (EndDate.DayNumber - StartDate.DayNumber + 1);
    
    public double TotalPrice { get; set; }
}
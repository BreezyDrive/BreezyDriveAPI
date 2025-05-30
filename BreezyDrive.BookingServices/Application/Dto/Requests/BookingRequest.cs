using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.BookingServices.Application.Dto.Requests;

public class BookingRequest : IMapFrom<Booking>
{
    public Guid CarId { get; set; }
    
    public Guid RentUserId { get; set; }
    
    public string? Location { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int TotalDays { get; set; }
    
    public double TotalPrice { get; set; }
}
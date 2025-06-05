using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.BookingServices.Application.Dto.Requests;

public class BookingRequest : IMapFrom<Booking>
{
    public Guid CarId { get; set; }
    
    public Guid RentUserId { get; set; }
    
    public string? Location { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public double TotalPrice { get; set; }
}
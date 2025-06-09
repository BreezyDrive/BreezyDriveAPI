using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.BookingServices.Application.Dto.Responses;

public class BookingResponse : IMapFrom<Booking>
{
    public Guid Id { get; set; }
    
    public Guid CarId { get; set; }
    
    public Guid RentUserId { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public int TotalDays { get; set; }
    
    public double TotalPrice { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.BookingServices.Domain.Entities;

[Table("Booking")]
public class Booking : BaseEntities
{
    public Guid CarId { get; set; }
    
    public Guid RentUserId { get; set; }
    
    public string Location { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public double TotalPrice { get; set; }
    
    
}
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BreezyDrive.BookingServices.Domain.Entities;

[Table("Bookings")]
public class Booking : BaseEntities
{
    [BsonRepresentation(BsonType.String)]
    public Guid CarId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid RentUserId { get; set; }
    
    public string Location { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public int TotalDays => (EndDate.DayNumber - StartDate.DayNumber + 1);
    
    public double TotalPrice { get; set; }
    
    
}
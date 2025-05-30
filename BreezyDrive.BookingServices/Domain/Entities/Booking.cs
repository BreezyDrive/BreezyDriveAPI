using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BreezyDrive.BookingServices.Domain.Entities;

[Table("Booking")]
public class Booking : BaseEntities
{
    [BsonRepresentation(BsonType.String)]
    public Guid CarId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid RentUserId { get; set; }
    
    public string Location { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int TotalDays { get; set; }
    
    public double TotalPrice { get; set; }
    
    
}
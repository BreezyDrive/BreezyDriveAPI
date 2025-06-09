using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BreezyDrive.BookingServices.Domain.Entities;

[Table("BookingSchedules")]
public class BookingSchedule : BaseEntities
{
    [BsonRepresentation(BsonType.String)]
    public Guid CarId { get; set; }
    
    public DateOnly Date { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid BookingId { get; set; }
    
}
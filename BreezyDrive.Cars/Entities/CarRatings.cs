using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities;

public class CarRatings
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public Guid CarId {get; set;}
    
    public required float Star {get; set;}
    
    public string? Comment {get; set;}
    
    [ForeignKey("CarId")]
    public virtual required Cars Car {get; set;}

}
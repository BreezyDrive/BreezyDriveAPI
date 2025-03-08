using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities;

public class CarRatings : IEntities
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public Guid CarId {get; set;}
    
    public required float Star {get; set;}
    
    public string? Comment {get; set;}
    
    [ForeignKey("CarId")]
    public virtual required Cars Car {get; set;}

}
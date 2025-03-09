using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Cars.Domain.Entities
{
    [Table("CarRatings")]
    public class CarRatings : BaseEntities
    {
        public Guid UserId { get; set; }
    
        public Guid CarId {get; set;}
    
        public required float Star {get; set;}
    
        public string? Comment {get; set;}
    
        [ForeignKey("CarId")]
        public virtual required Cars Car {get; set;}

    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("CarFeatures")]
    public class CarFeatures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    
        public Guid CarId { get; set; }
    
        public Guid FeatureId { get; set; }
    
        [ForeignKey("CarId")]
        public virtual required Cars Car { get; set; }
    
        [ForeignKey("FeatureId")]
        public virtual required Features Feature { get; set; }
    
    
    }
}


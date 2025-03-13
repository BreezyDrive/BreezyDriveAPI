using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.CarServices.Domain.Entities
{
    [Table("CarFeatures")]
    public class CarFeatures : BaseEntities
    {
        public Guid CarId { get; set; }
    
        public Guid FeatureId { get; set; }
    
        [ForeignKey("CarId")]
        public virtual required Cars Car { get; set; }
    
        [ForeignKey("FeatureId")]
        public virtual required Features Feature { get; set; }
    }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities;

public class CarFeatures
{
    public Guid Id { get; set; }
    
    public Guid CarId { get; set; }
    
    public Guid FeatureId { get; set; }
    
    [ForeignKey("CarId")]
    public virtual required Cars Car { get; set; }
    
    [ForeignKey("FeatureId")]
    public virtual required Features Feature { get; set; }
    
    
}
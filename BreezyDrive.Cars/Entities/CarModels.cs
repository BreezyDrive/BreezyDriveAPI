using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities;

public class CarModels
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public Guid BrandId { get; set; }
    
    public required string Name { get; set; }
    
    public required int ReleaseYear { get; set; }
    
    [ForeignKey("BrandId")]
    public virtual required CarBrands CarBrand { get; set; }

    
}
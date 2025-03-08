using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("CarModels")]
    public class CarModels : BaseEntities
    {
        public Guid BrandId { get; set; }
    
        public required string Name { get; set; }
    
        public required int ReleaseYear { get; set; }
    
        [ForeignKey("BrandId")]
        public virtual required CarBrands CarBrand { get; set; }

    
    }
}


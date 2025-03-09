using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.CarServices.Domain.Entities
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


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Cars.Domain.Entities
{
    [Table("CarBrands")]
    public class CarBrands : BaseEntities
    {
        public required string Name { get; set; }
    }
}


using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("CarBrands")]
    public class CarBrands : BaseEntities
    {
        public required string Name { get; set; }
    }
}


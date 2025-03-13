using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.CarServices.Domain.Entities
{
    [Table("CarBrands")]
    public class CarBrands : BaseEntities
    {
        public required string Name { get; set; }
    }
}


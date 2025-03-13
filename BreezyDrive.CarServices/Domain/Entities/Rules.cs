using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.CarServices.Domain.Entities
{
    [Table("Rules")]
    public class Rules : BaseEntities
    {
        public required string Name { get; set; }
    }
}


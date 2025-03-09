using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.CarServices.Domain.Entities
{
    [Table("Features")]
    public class Features : BaseEntities
    {
        public required string Name { get; set; }
    }
}


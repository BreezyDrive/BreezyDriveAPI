using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("Features")]
    public class Features : BaseEntities
    {
        public required string Name { get; set; }
    }
}


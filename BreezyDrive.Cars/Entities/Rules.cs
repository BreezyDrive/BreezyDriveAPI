using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("Rules")]
    public class Rules : BaseEntities
    {
        public required string Name { get; set; }
    }
}


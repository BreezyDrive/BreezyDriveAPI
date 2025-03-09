using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Cars.Domain.Entities
{
    [Table("Rules")]
    public class Rules : BaseEntities
    {
        public required string Name { get; set; }
    }
}


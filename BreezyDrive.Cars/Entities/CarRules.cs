using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("CarRules")]
    public class CarRules : BaseEntities
    {
        public Guid CarId { get; set; }

        public Guid RuleId { get; set; }

        [ForeignKey("CarId")]
        public virtual required Cars Car { get; set; }

        [ForeignKey("RuleId")]
        public virtual required Rules Rule { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities;

public class CarRules
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }   
    public Guid CarId { get; set; }
    public Guid RuleId { get; set; }
    
    [ForeignKey("CarId")]
    public virtual required Cars Car { get; set; }
    
    [ForeignKey("RuleId")]
    public virtual required Rules Rule { get; set; }
}
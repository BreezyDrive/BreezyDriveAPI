using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.CommonService.Domain.Entities;

namespace BreezyDrive.UserServices.Domain.Entities
{
    [Table("Roles")]
    public class Roles : BaseEntities
    {
        public string Name { get; set; }
    }
}

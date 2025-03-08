using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BreezyDrive.Common.Domain.Entities;

namespace BreezyDrive.Users.Domain.Entities
{
    [Table("Roles")]
    public class Roles : BaseEntities
    {
        public string Name { get; set; }
    }
}

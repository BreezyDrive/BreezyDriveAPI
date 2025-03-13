using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Domain.Entities
{
    public abstract class BaseEntities
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

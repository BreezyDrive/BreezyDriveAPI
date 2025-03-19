using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EventContracts.Events.UserEvents.Response
{
    public class CheckGoogleExistResponseEvent
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public string Avatar { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}

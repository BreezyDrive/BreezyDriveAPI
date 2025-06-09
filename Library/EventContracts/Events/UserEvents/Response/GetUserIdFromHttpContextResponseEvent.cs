using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EventContracts.Events.UserEvents.Response
{
    public class GetUserIdFromHttpContextResponseEvent
    {
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
    }
}

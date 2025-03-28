using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EventContracts.Events.UserEvents.Response
{
    public class GetUserIdResponseEvent
    {
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
    }
}

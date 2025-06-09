using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EventContracts.Events.UserEvents.Request
{
    public class GetUserIdFromHttpContextRequestEvent
    {
        public string JwtToken { get; set; }
    }
}

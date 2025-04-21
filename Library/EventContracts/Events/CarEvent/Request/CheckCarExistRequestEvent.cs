using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EventContracts.Events.CarEvent.Request
{
    public class CheckCarExistRequestEvent
    {
        public Guid CarId { get; set; }
    }
}

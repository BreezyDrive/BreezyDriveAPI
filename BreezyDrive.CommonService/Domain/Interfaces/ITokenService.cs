using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<Guid> GetUserIdAsync();
    }
}

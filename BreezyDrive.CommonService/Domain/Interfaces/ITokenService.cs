using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BreezyDrive.CommonService.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetTokenFromHttpContext(IHttpContextAccessor httpContextAccessor);

        Task<Guid> GetUserIdFromHttpContext(IHttpContextAccessor httpContextAccessor);


    }
}

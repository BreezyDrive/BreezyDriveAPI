using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace BreezyDrive.CommonService.Utils
{
    public class TokenService : ITokenService
    {
        private readonly IRequestClient<GetUserIdRequestEvent> _requestClient;

        public TokenService(IHttpContextAccessor httpContextAccessor, IRequestClient<GetUserIdRequestEvent> requestClient)
        {
 
            _requestClient = requestClient;
        }

        public string GetTokenFromHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
    
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new UnauthorizedAccessException("Token không được cung cấp.");
            }

            return authorizationHeader.Replace("Bearer ", "");
        }
        
    }
}

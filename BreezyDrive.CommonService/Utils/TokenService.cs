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
        private readonly IRequestClient<GetUserIdFromHttpContextRequestEvent> _userIdRequestClient;



        public TokenService(IHttpContextAccessor httpContextAccessor, IRequestClient<GetUserIdFromHttpContextRequestEvent> userIdRequestClient)
        {
 
            _userIdRequestClient = userIdRequestClient;

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
        
        public async Task<Guid> GetUserIdFromHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            var token = GetTokenFromHttpContext(httpContextAccessor);

            var response = await _userIdRequestClient.GetResponse<GetUserIdFromHttpContextResponseEvent>(
                new GetUserIdFromHttpContextRequestEvent
                {
                    JwtToken = token
                });
            
            return response.Message.UserId;
            
        }
        
    }
}

using System;
using System.Collections.Generic;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestClient<GetUserIdRequestEvent> _requestClient;

        public TokenService(IHttpContextAccessor httpContextAccessor, IRequestClient<GetUserIdRequestEvent> requestClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestClient = requestClient;
        }

        public async Task<Guid> GetUserIdAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || !httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new CustomExceptions.InternalServerErrorException("Authorization header is missing.");
            }

            string authorizationHeader = httpContext.Request.Headers["Authorization"];
            string jwtToken = authorizationHeader.Replace("Bearer ", "").Trim();

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                throw new CustomExceptions.InternalServerErrorException("Invalid Authorization header format.");
            }

            // Gửi token qua RabbitMQ để AuthenticationService xử lý
            var response = await _requestClient.GetResponse<GetUserIdResponseEvent>(new GetUserIdRequestEvent { JwtToken = jwtToken });

            if (!response.Message.IsSuccess)
            {
                throw new CustomExceptions.InternalServerErrorException("Invalid JWT token.");
            }

            return response.Message.UserId;
        }
    }
}

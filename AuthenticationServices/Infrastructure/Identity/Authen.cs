using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Library.EventContracts.Events.UserEvents.Response;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.AuthenticationServices.Infrastructure.Identity
{
    public class Authen : IAuthentication
    {
        private readonly IConfiguration _configuration;

        public Authen(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(GetUserResponseEvent users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, users.Phone),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", users.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", users.RoleId)),
                new Claim("username", users.FullName),
                new Claim("avatar",users.Avatar)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GetUserIdFromHttpContext(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new CustomExceptions.InternalServerErrorException("Authorization header is missing.");
            }

            string authorizationHeader = httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new CustomExceptions.InternalServerErrorException("Invalid Authorization header format.");
            }

            string jwtToken = authorizationHeader["Bearer ".Length..];

            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(jwtToken))
            {
                throw new CustomExceptions.InternalServerErrorException("Invalid JWT token format.");
            }

            try
            {
                var token = tokenHandler.ReadJwtToken(jwtToken);
                var idClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");

                if (idClaim == null || string.IsNullOrWhiteSpace(idClaim.Value))
                {
                    throw new CustomExceptions.InternalServerErrorException("User ID claim not found in token.");
                }

                if (!Guid.TryParse(idClaim.Value, out Guid userId))
                {
                    throw new CustomExceptions.InternalServerErrorException("Invalid GUID format in User ID claim.");
                }

                return userId;
            }
            catch (Exception ex)
            {
                throw new CustomExceptions.InternalServerErrorException($"Error parsing token: {ex.Message}");
            }
        }

    }
}

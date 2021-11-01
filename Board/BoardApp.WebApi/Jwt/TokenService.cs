using BoardApp.Common.Models;
using BoardApp.WebApi.Exceptions;
using BoardApp.WebApi.Jwt.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoardApp.WebApi.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;

        public TokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(UserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(_options.Issuer,
                                             _options.Issuer,
                                             claims,
                                             expires: DateTime.Now.AddSeconds(_options.ExpiryDurationSeconds),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int GetUserId(HttpContext httpContext)
        {
            int id = 0;
            if (httpContext.User.Identity is ClaimsIdentity identity)
            {
                int.TryParse(identity.FindFirst("Id")?.Value, out id);
            }

            if (id == 0)
            {
                throw new AuthorizeException();
            }

            return id;
        }
    }
}

using BoardApp.Common.Models;
using Microsoft.AspNetCore.Http;

namespace BoardApp.WebApi.Jwt
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
        int GetUserId(HttpContext httpContext);
    }
}

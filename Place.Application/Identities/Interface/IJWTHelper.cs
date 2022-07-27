
using System.Security.Claims;
using Place.Domain.Models;

namespace Place.Application.Identities.Interface
{
    public interface IJWTHelper
    {
        ClaimsPrincipal GetPrincipal(string token, string jwtSecurityKey);
        bool ValidateToken(string token, out User user, string jwtSecurityKey);
        string CreateToken(User user, string jwtSecurityKey);
    }
}

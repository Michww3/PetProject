using PetProject.Entitys;
using System.Security.Claims;

namespace PetProject.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}

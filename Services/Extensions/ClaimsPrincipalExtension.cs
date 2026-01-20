using PetProject.Exceptions;
using System.Security.Claims;

namespace PetProject.Services.Extension
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null || !Guid.TryParse(claim.Value, out var userId))
                throw new UnauthorizedException("Invalid or missing token");

            return userId;
        }
    }
}

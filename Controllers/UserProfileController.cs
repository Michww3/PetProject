using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.DTOs;
using PetProject.Entitys;
using PetProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UsersController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized(new { message = "Invalid or missing token" });

            var user = await _userProfileService.GetProfileAsync(userId);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email
            });
        }
    }
}

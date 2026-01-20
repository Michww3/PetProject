using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Response;
using PetProject.Services.Extension;
using PetProject.Services.Interfaces;
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
        public async Task<ActionResult<UserResponse>> GetProfile()
        {
            var userId = User.GetUserId();

            var user = await _userProfileService.GetProfileAsync(userId);

            return Ok(new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            });
        }
    }
}

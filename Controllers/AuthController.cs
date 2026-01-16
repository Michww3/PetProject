using Microsoft.AspNetCore.Mvc;
using PetProject.Auth;
using PetProject.Services.Interfaces;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(request);
            //change to Created
            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email
            });
        }
    }
}

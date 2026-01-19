using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Request;
using PetProject.DTOs.Response;
using PetProject.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [SwaggerTag("Аутентификация и авторизация пользователей")]
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
        /// <remarks>
        /// Создаёт нового пользователя и возвращает базовую информацию.
        ///
        /// Пример запроса:
        ///
        ///     POST /api/auth/register
        ///     {
        ///         "username": "john_doe",
        ///         "email": "john@example.com",
        ///         "password": "StrongPassword123!"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Данные для регистрации</param>
        /// <response code="201">Пользователь успешно создан</response>
        /// <response code="409">Пользователь с таким Email или Username уже существует</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(request);

            return Created(string.Empty, new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            });
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <remarks>
        /// Выполняет вход пользователя и возвращает JWT токен.
        ///
        /// Пример запроса:
        ///
        ///     POST /api/auth/login
        ///     {
        ///         "email": "john@example.com",
        ///         "password": "StrongPassword123!"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Данные для входа</param>
        /// <response code="200">Успешная аутентификация</response>
        /// <response code="401">Неверный логин или пароль</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);

            return Ok(new TokenResponse
            {
                Token = token,
                TokenType = "Bearer",
                ExpiresIn = 86400
            });
        }
    }
}

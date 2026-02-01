using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Response;
using PetProject.Services.Extensions;
using PetProject.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    [SwaggerTag("Работа с профилем текущего пользователя")]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UsersController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Получить профиль текущего пользователя
        /// </summary>
        /// <remarks>
        /// Возвращает информацию о пользователе, определённом по JWT-токену,
        /// переданному в заголовке Authorization.
        ///
        /// Пример запроса:
        ///
        ///     GET /api/users/profile
        ///     Authorization: Bearer {token}
        ///
        /// </remarks>
        /// <response code="200">Профиль пользователя успешно получен</response>
        /// <response code="401">Пользователь не авторизован или токен некорректен</response>
        [HttpGet("profile")]
        [SwaggerOperation(
            Summary = "Получить профиль пользователя",
            Description = "Возвращает Id, Username и Email текущего авторизованного пользователя"
        )]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

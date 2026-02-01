using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Request;
using PetProject.DTOs.Response;
using PetProject.Services.Extensions;
using PetProject.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Authorize]
    [Produces("application/json")]
    [SwaggerTag("Проекты")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Получить список проектов текущего пользователя
        /// </summary>
        /// <remarks>
        /// Возвращает все проекты, принадлежащие текущему пользователю.
        ///
        /// Пользователь может работать только со своими проектами
        /// (Project.UserId == CurrentUserId).
        /// </remarks>
        /// <response code="200">Список проектов успешно получен</response>
        /// <response code="401">Пользователь не авторизован</response>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Получить проекты пользователя",
            Description = "Возвращает список всех проектов, принадлежащих текущему пользователю"
        )]
        [ProducesResponseType(typeof(IEnumerable<ProjectResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAll()
        {
            var userId = User.GetUserId();

            var projects = await _projectService.GetAllAsync(userId);

            return Ok(projects.Select(p => new ProjectResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }));
        }

        /// <summary>
        /// Создать новый проект
        /// </summary>
        /// <remarks>
        /// Создаёт новый проект и автоматически привязывает его
        /// к текущему пользователю.
        ///
        /// Базовая валидация:
        /// - Name не может быть пустым
        /// </remarks>
        /// <param name="request">Данные для создания проекта</param>
        /// <response code="201">Проект успешно создан</response>
        /// <response code="400">Ошибка валидации данных</response>
        /// <response code="401">Пользователь не авторизован</response>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создать проект",
            Description = "Создаёт новый проект и привязывает его к текущему пользователю"
        )]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProjectResponse>> Create(
            [FromBody] ProjectRequest request)
        {
            var userId = User.GetUserId();

            var project = await _projectService.CreateAsync(userId, request);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = project.Id },
                new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description
                });
        }

        /// <summary>
        /// Обновить проект
        /// </summary>
        /// <remarks>
        /// Обновляет существующий проект.
        ///
        /// Доступ разрешён только если проект принадлежит
        /// текущему пользователю.
        /// </remarks>
        /// <param name="id">Идентификатор проекта</param>
        /// <param name="request">Новые данные проекта</param>
        /// <response code="200">Проект успешно обновлён</response>
        /// <response code="400">Ошибка валидации данных</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Проект не найден</response>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Обновить проект",
            Description = "Обновляет проект, если он принадлежит текущему пользователю"
        )]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectResponse>> Update(
            Guid id,
            [FromBody] ProjectRequest request)
        {
            var userId = User.GetUserId();

            var project = await _projectService.UpdateAsync(userId, id, request);

            return Ok(new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            });
        }

        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <remarks>
        /// Удаляет проект, если он принадлежит текущему пользователю.
        ///
        /// При удалении проекта также будут удалены
        /// связанные графы (если настроено каскадное удаление).
        /// </remarks>
        /// <param name="id">Идентификатор проекта</param>
        /// <response code="204">Проект успешно удалён</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Проект не найден</response>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(
            Summary = "Удалить проект",
            Description = "Удаляет проект текущего пользователя"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();

            await _projectService.DeleteAsync(userId, id);

            return NoContent();
        }
    }
}

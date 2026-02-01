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
    [Route("api/nodegraphs")]
    [Authorize]
    [Produces("application/json")]
    [SwaggerTag("Графы нод (NodeGraphs)")]
    public class NodeGraphsController : ControllerBase
    {
        private readonly INodeGraphService _nodeGraphService;

        public NodeGraphsController(INodeGraphService nodeGraphService)
        {
            _nodeGraphService = nodeGraphService;
        }

        /// <summary>
        /// Получить список всех графов текущего пользователя
        /// </summary>
        /// <remarks>
        /// Возвращает все графы, принадлежащие проектам текущего пользователя.
        ///
        /// Проверка доступа:
        /// NodeGraph → Project → User
        /// </remarks>
        /// <response code="200">Список графов успешно получен</response>
        /// <response code="401">Пользователь не авторизован</response>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Получить все графы пользователя",
            Description = "Возвращает список всех графов нод, принадлежащих текущему пользователю"
        )]
        [ProducesResponseType(typeof(IEnumerable<NodeGraphListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<NodeGraphListResponse>>> GetAll()
        {
            var userId = User.GetUserId();

            var graphs = await _nodeGraphService.GetAllAsync(userId);

            var result = graphs.Select(g => new NodeGraphListResponse
            {
                Id = g.Id,
                Name = g.Name,
                ProjectId = g.ProjectId,
            });

            return Ok(result);
        }

        /// <summary>
        /// Получить граф по идентификатору
        /// </summary>
        /// <remarks>
        /// Возвращает конкретный граф нод.
        ///
        /// Доступ разрешён только если граф принадлежит проекту текущего пользователя.
        /// </remarks>
        /// <param name="id">Идентификатор графа</param>
        /// <response code="200">Граф найден</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Граф не найден</response>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Получить граф по Id",
            Description = "Возвращает граф нод при условии, что он принадлежит текущему пользователю"
        )]
        [ProducesResponseType(typeof(NodeGraphResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NodeGraphResponse>> GetById(Guid id)
        {
            var userId = User.GetUserId();

            var graph = await _nodeGraphService.GetByIdAsync(userId, id);

            return Ok(new NodeGraphResponse
            {
                Id = graph.Id,
                Name = graph.Name,
                ProjectId = graph.ProjectId,
                JsonData = graph.JsonData
            });
        }

        /// <summary>
        /// Создать новый граф нод
        /// </summary>
        /// <remarks>
        /// Создаёт новый граф в указанном проекте.
        ///
        /// Проверки:
        /// - проект существует
        /// - проект принадлежит текущему пользователю
        /// </remarks>
        /// <param name="request">Данные для создания графа</param>
        /// <response code="201">Граф успешно создан</response>
        /// <response code="400">Ошибка валидации данных</response>
        /// <response code="401">Пользователь не авторизован</response>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создать граф",
            Description = "Создаёт новый граф нод в проекте текущего пользователя"
        )]
        [ProducesResponseType(typeof(NodeGraphResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NodeGraphResponse>> Create([FromBody] NodeGraphRequest request)
        {
            var userId = User.GetUserId();

            var graph = await _nodeGraphService.CreateAsync(userId, request);

            return CreatedAtAction(nameof(GetById), new { id = graph.Id }, new NodeGraphResponse
            {
                Id = graph.Id,
                Name = graph.Name,
                ProjectId = graph.ProjectId,
                JsonData = graph.JsonData
            });
        }

        /// <summary>
        /// Обновить существующий граф
        /// </summary>
        /// <remarks>
        /// Обновляет данные графа.
        ///
        /// Доступ разрешён только владельцу проекта.
        /// </remarks>
        /// <param name="id">Идентификатор графа</param>
        /// <param name="request">Данные для обновления</param>
        /// <response code="200">Граф успешно обновлён</response>
        /// <response code="400">Ошибка валидации</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Граф не найден</response>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Обновить граф",
            Description = "Обновляет граф нод, если он принадлежит текущему пользователю"
        )]
        [ProducesResponseType(typeof(NodeGraphResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NodeGraphResponse>> Update(
            Guid id,
            [FromBody] NodeGraphUpdateRequest request)
        {
            var userId = User.GetUserId();

            var graph = await _nodeGraphService.UpdateAsync(userId, id, request);

            return Ok(new NodeGraphResponse
            {
                Id = graph.Id,
                Name = graph.Name,
                ProjectId = graph.ProjectId,
                JsonData = graph.JsonData
            });
        }

        /// <summary>
        /// Удалить граф
        /// </summary>
        /// <remarks>
        /// Удаляет граф нод, если он принадлежит текущему пользователю.
        /// </remarks>
        /// <param name="id">Идентификатор графа</param>
        /// <response code="204">Граф успешно удалён</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Граф не найден</response>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(
            Summary = "Удалить граф",
            Description = "Удаляет граф нод текущего пользователя"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();

            await _nodeGraphService.DeleteAsync(userId, id);

            return NoContent();
        }
    }
}

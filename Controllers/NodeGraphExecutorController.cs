using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [SwaggerTag("Выполнение графов нод")]
    public class NodeGraphExecutorController : ControllerBase
    {
        private readonly INodeGraphService _nodeGraphService;
        private readonly INodeExecutorService _nodeExecutorService;

        public NodeGraphExecutorController(
            INodeGraphService nodeGraphService,
            INodeExecutorService nodeExecutorService)
        {
            _nodeGraphService = nodeGraphService;
            _nodeExecutorService = nodeExecutorService;
        }

        /// <summary>
        /// Выполнение графа нод
        /// </summary>
        /// <remarks>
        /// Выполняет граф нод по его идентификатору.
        ///
        /// Перед выполнением производится проверка:
        /// - граф существует
        /// - граф принадлежит проекту текущего пользователя
        ///
        /// Входные данные графа берутся из поля <c>JsonData</c>.
        ///
        /// Пример запроса:
        ///
        ///     POST /api/nodegraphs/{id}/execute
        ///
        /// Пример ответа:
        ///
        ///     {
        ///         "graphId": "c1a2b3c4-1111-2222-3333-444455556666",
        ///         "result": 42
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Идентификатор графа</param>
        /// <response code="200">Граф успешно выполнен</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Граф не найден</response>
        /// <response code="500">Ошибка выполнения графа</response>
        [HttpPost("{id:guid}/execute")]
        [SwaggerOperation(
            Summary = "Выполнение графа",
            Description = "Запускает выполнение графа нод и возвращает результат последней ноды"
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NodeGraphExecutionResponse>> Execute(Guid id)
        {
            var userId = User.GetUserId();

            //Получаем граф с проверкой владения
            var graph = await _nodeGraphService.GetByIdAsync(userId, id);

            var result = await _nodeExecutorService.ExecuteGraphAsync(graph.JsonData);

            return Ok(new NodeGraphExecutionResponse
            {
                GraphId = graph.Id,
                Result = result
            });
        }
    }
}

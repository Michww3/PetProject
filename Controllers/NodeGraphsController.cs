using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Request;
using PetProject.DTOs.Response;
using PetProject.Services.Extension;
using PetProject.Services.Interfaces;

namespace PetProject.Controllers
{
    //to project controller veiw
    [ApiController]
    [Route("api/nodegraphs")]
    [Authorize]
    public class NodeGraphsController : ControllerBase
    {
        private readonly INodeGraphService _nodeGraphService;

        public NodeGraphsController(INodeGraphService nodeGraphService)
        {
            _nodeGraphService = nodeGraphService;
        }

        // GET /api/nodegraphs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeGraphResponse>>> GetAll()
        {
            var userId = User.GetUserId();

            var graphs = await _nodeGraphService.GetAllAsync(userId);

            var result = graphs.Select(g => new NodeGraphResponse
            {
                Id = g.Id,
                Name = g.Name,
                ProjectId = g.ProjectId,
                //?
                JsonData = g.JsonData
            });

            return Ok(result);
        }

        // GET /api/nodegraphs/{id}
        [HttpGet("{id:guid}")]
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

        // POST /api/nodegraphs
        [HttpPost]
        public async Task<ActionResult<NodeGraphResponse>> Create(NodeGraphRequest request)
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

        // PUT /api/nodegraphs/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<NodeGraphResponse>> Update(Guid id, NodeGraphUpdateRequest request)
        {
            var userId = User.GetUserId();

            var graph = await _nodeGraphService.UpdateAsync(userId, id, request);

            return Ok(new NodeGraphResponse
            {
                Id = graph.Id,
                Name = graph.Name,
                ProjectId = graph.ProjectId,
                JsonData = graph .JsonData
            });
        }

        // DELETE /api/nodegraphs/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();

            await _nodeGraphService.DeleteAsync(userId, id);

            return NoContent();
        }
    }
}

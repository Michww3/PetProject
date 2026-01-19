using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Request;
using PetProject.DTOs.Response;
using PetProject.Exceptions;
using PetProject.Services.Interfaces;
using System.Security.Claims;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        private Guid GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null || !Guid.TryParse(claim.Value, out var userId))
                throw new UnauthorizedException("Invalid token");

            return userId;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAll()
        {
            var userId = GetUserId();

            var projects = await _projectService.GetAllAsync(userId);

            return Ok(projects.Select(p => new ProjectResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }));
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> Create(ProjectRequest request)
        {
            var userId = GetUserId();

            var project = await _projectService.CreateAsync(userId, request);

            return CreatedAtAction(nameof(GetAll), new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProjectResponse>> Update(Guid id, ProjectRequest request)
        {
            var userId = GetUserId();

            var project = await _projectService.UpdateAsync(userId, id, request);

            return Ok(new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();

            await _projectService.DeleteAsync(userId, id);

            return NoContent();
        }
    }
}
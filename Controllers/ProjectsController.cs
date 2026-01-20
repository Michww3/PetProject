using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.DTOs.Request;
using PetProject.DTOs.Response;
using PetProject.Services.Extension;
using PetProject.Services.Interfaces;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> Create(ProjectRequest request)
        {
            var userId = User.GetUserId();

            var project = await _projectService.CreateAsync(userId, request);

            return CreatedAtAction(nameof(GetAll),
                new { id = project.Id },
                new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description
                });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProjectResponse>> Update(Guid id, ProjectRequest request)
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();

            await _projectService.DeleteAsync(userId, id);

            return NoContent();
        }
    }
}
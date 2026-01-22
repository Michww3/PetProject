using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.DTOs.Request;
using PetProject.Entitys;
using PetProject.Exceptions;
using PetProject.Services.Interfaces;

namespace PetProject.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Project> _projectRepo;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepo = unitOfWork.Repository<Project>();
        }

        private async Task<Project> GetUserProject(Guid userId, Guid projectId)
        {
            var project = await _projectRepo.FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            return project ?? throw new NotFoundException("Project not found");
        }

        public async Task<IEnumerable<Project>> GetAllAsync(Guid userId)
        {
            return await _projectRepo.GetListAsync(p => p.UserId == userId);
        }

        public async Task<Project> CreateAsync(Guid userId, ProjectRequest request)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = request.Name,
                Description = request.Description,
            };

            await _projectRepo.CreateAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return project;
        }

        public async Task<Project> UpdateAsync(Guid userId, Guid projectId, ProjectRequest request)
        {
            var project =  await GetUserProject(userId, projectId);

            project.Name = request.Name;
            project.Description = request.Description;

            await _projectRepo.UpdateAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return project;
        }

        public async Task DeleteAsync(Guid userId, Guid projectId)
        {
            var project = await GetUserProject(userId, projectId);

            await _projectRepo.DeleteAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

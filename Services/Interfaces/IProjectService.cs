using PetProject.DTOs.Request;
using PetProject.Entitys;

namespace PetProject.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync(Guid userId);
        Task<Project> CreateAsync(Guid userId, ProjectRequest request);
        Task<Project> UpdateAsync(Guid userId, Guid projectId, ProjectRequest request);
        Task DeleteAsync(Guid userId, Guid projectId);
    }
}
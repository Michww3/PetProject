using PetProject.DTOs.Request;
using PetProject.Entitys;

namespace PetProject.Services.Interfaces
{
    public interface INodeGraphService
    {
        Task<IEnumerable<NodeGraph>> GetAllAsync(Guid userId);
        Task<NodeGraph> GetByIdAsync(Guid userId, Guid graphId);
        Task<NodeGraph> CreateAsync(Guid userId, NodeGraphRequest request);
        Task<NodeGraph> UpdateAsync(Guid userId, Guid graphId, NodeGraphRequest request);
        Task DeleteAsync(Guid userId, Guid graphId);
    }
}

using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.DTOs.Request;
using PetProject.Entitys;
using PetProject.Exceptions;
using PetProject.Services.Interfaces;

namespace PetProject.Services
{
    public class NodeGraphService : INodeGraphService
    {
        private readonly IGenericRepository<NodeGraph> _graphRepo;
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly IUnitOfWork _unitOfWork;

        public NodeGraphService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _graphRepo = unitOfWork.Repository<NodeGraph>();
            _projectRepo = unitOfWork.Repository<Project>();
        }

        private async Task<NodeGraph> GetUserGraph(Guid userId, Guid graphId)
        {
            var graph = await _graphRepo
                .FirstOrDefaultAsync(g =>
                    g.Id == graphId &&
                    g.Project.UserId == userId);

            if (graph == null)
                throw new NotFoundException("NodeGraph not found");

            return graph;
        }

        public async Task<IEnumerable<NodeGraph>> GetAllAsync(Guid userId)
        {
            return await _graphRepo.GetListAsync(
                g => g.Project.UserId == userId
            );
        }

        public async Task<NodeGraph> GetByIdAsync(Guid userId, Guid graphId)
        {
            return await GetUserGraph(userId, graphId);
        }

        public async Task<NodeGraph> CreateAsync(Guid userId, NodeGraphRequest request)
        {
            var projectExists = await _projectRepo.ExistsAsync(
                p => p.Id == request.ProjectId && p.UserId == userId
            );

            if (!projectExists)
                throw new NotFoundException("Project not found");

            var graph = new NodeGraph
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ProjectId = request.ProjectId,
                JsonData = request.JsonData
            };

            await _graphRepo.CreateAsync(graph);
            await _unitOfWork.SaveChangesAsync();

            return graph;
        }

        public async Task<NodeGraph> UpdateAsync(Guid userId, Guid graphId, NodeGraphUpdateRequest request)
        {
            var graph = await GetUserGraph(userId, graphId);

            graph.JsonData = request.JsonData;

            await _graphRepo.UpdateAsync(graph);
            await _unitOfWork.SaveChangesAsync();

            return graph;
        }

        public async Task DeleteAsync(Guid userId, Guid graphId)
        {
            var graph = await GetUserGraph(userId, graphId);

            await _graphRepo.DeleteAsync(graph);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

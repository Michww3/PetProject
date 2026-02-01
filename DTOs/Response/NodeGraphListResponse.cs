namespace PetProject.DTOs.Response
{
    public class NodeGraphListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}

namespace PetProject.DTOs.Response
{
    public class NodeGraphResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string JsonData { get; set; } = null!;
    }
}

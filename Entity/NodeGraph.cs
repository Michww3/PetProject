namespace PetProject.DTOs
{
    public class NodeGraph
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string JSONData { get; set; } = null!;
    }
}

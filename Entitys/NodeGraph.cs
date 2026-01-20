namespace PetProject.Entitys
{
    public class NodeGraph
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public Guid ProjectId { get; set; }
        public string JsonData { get; set; } = null!;

        public Project Project { get; set; } = null!;
    }
}

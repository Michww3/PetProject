namespace PetProject.Entitys
{
    public class Project
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public User User { get; set; } = null!;
        public ICollection<NodeGraph> NodeGraphs { get; set; } = new List<NodeGraph>();

    }
}
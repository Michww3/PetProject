namespace PetProject.DTOs
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public User Guid { get; set; } = null!;
    }
}

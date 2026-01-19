namespace PetProject.DTOs.Response
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

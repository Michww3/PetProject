namespace PetProject.Entitys
{
    public class CustomNodeType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string InputDefenition { get; set; } = null!;
        public string OutputDefenition { get; set; } = null!;
    }
}

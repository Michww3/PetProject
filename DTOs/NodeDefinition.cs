namespace PetProject.DTOs
{
    public class NodeDefinition
    {
        public string Id { get; set; } = null!;
        public string Type { get; set; } = null!;
        public Dictionary<string, object> Inputs { get; set; } = new();
    }
}

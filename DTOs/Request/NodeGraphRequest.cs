using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class NodeGraphRequest
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string JsonData { get; set; } = null!;
    }
}

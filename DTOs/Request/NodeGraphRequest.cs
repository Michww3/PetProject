using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class NodeGraphRequest
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(1000)]
        public string JsonData { get; set; } = null!;
    }
}

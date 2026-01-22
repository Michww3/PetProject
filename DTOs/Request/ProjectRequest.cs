using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class ProjectRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}

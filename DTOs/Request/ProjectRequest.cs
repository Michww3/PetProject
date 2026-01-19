using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class ProjectRequest
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class NodeGraphUpdateRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        public string JsonData { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class NodeGraphUpdateRequest
    {
        [Required]
        public string JsonData { get; set; } = null!;
    }
}

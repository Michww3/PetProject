using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class NodeGraphRequest
    {
        [Required]
        [SwaggerSchema("ID проекта, к которому будет привязан граф")]
        public Guid ProjectId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        [SwaggerSchema("Название графа")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        [SwaggerSchema("JSON-описание структуры графа (ноды и связи)")]
        public string JsonData { get; set; } = null!;
    }
}
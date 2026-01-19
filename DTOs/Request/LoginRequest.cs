using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class LoginRequest
    {
        /// <summary>
        /// Email пользователя
        /// </summary>
        /// <example>john@example.com</example>
        [Required]
        [EmailAddress]
        [MinLength(3)]
        public String Email { get; set; } = null!;
        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>StrongPassword123!</example>
        [Required]
        [MinLength (8)]
        public String Password { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace PetProject.DTOs.Request
{
    public class RegisterRequest
    {
        /// <summary>
        /// Имя пользователя (уникальное)
        /// </summary>
        /// <example>john_doe</example>
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Username { get; set; } = null!;

        /// <summary>
        /// Email пользователя
        /// </summary>
        /// <example>john@example.com</example>
        [Required]
        [EmailAddress]
        [MinLength(2)]
        [MaxLength(255)]
        public string Email { get; set; } = null!;


        /// <summary>
        /// Пароль пользователя
        /// </summary>
        /// <example>StrongPassword123!</example>
        [Required]
        [MinLength(8)]
        [MaxLength(255)]
        public string Password { get; set; } = null!;
    }
}

namespace PetProject.DTOs.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}

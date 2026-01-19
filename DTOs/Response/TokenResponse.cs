namespace PetProject.DTOs.Response
{
    public class TokenResponse
    {
        public string Token { get; set; } = null!;
        public string TokenType { get; set; } = null!;
        public int ExpiresIn { get; set; }
    }
}

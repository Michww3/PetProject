using PetProject.Auth;
using PetProject.DTOs;

namespace PetProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest registerRequest);
        Task<User> LoginAsync(string email, string password);
    }
}

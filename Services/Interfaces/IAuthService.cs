using PetProject.DTOs.Request;
using PetProject.Entitys;

namespace PetProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest registerRequest);
        Task<string> LoginAsync(LoginRequest loginRequest);
    }
}

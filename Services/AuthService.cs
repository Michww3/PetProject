using PetProject.Auth;
using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.DTOs;
using PetProject.Services.Interfaces;

namespace PetProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            var username = request.Username.Trim();
            var email = request.Email.Trim().ToLowerInvariant();

            var userRepo = _unitOfWork.Repository<User>();

            if (await userRepo.ExistsAsync(u => u.Username == username))
                throw new InvalidOperationException("Username already exists");

            if (await userRepo.ExistsAsync(u => u.Email == email))
                throw new InvalidOperationException("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await userRepo.Create(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

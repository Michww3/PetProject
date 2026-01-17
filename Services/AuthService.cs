using Microsoft.IdentityModel.Tokens;
using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.DTOs;
using PetProject.Entitys;
using PetProject.Exceptions;
using PetProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Repository<User>();
            _jwtTokenService = jwtTokenService;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            var username = request.Username.Trim();
            var email = request.Email.Trim().ToLowerInvariant();

            if (await _userRepository.ExistsAsync(u => u.Username == username))
                throw new ConflicttException("Username already exists");

            if (await _userRepository.ExistsAsync(u => u.Email == email))
                throw new ConflicttException("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _userRepository
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new UnauthorizedException("User not found. (Are you register?)");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid password");

            return _jwtTokenService.GenerateToken(user);
        }
    }
}

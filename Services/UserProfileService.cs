using PetProject.DataAccess.DbPatterns.Interfaces;
using PetProject.Entitys;
using PetProject.Exceptions;
using PetProject.Services.Interfaces;

namespace PetProject.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetProfileAsync(Guid guid)
        {
            var user = await _unitOfWork.Repository<User>().Get(guid);
            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }
    }
}

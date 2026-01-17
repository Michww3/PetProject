using PetProject.Entitys;

namespace PetProject.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<User> GetProfileAsync(Guid guid);
    }
}

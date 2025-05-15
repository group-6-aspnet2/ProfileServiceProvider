using Domain.Models;

namespace Business.Interfaces;

public interface IProfileService
{
    Task<UserProfileModel?> GetAsync(int userId);
    Task<UserProfileModel> CreateAsync(CreateProfileModel model, int userId);
}

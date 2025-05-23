using Domain.Models;

namespace Business.Interfaces;

public interface IProfileService
{
    Task<UserProfileModel?> GetAsync(int userId);
    Task<CreateProfileResult> CreateAsync(CreateProfileModel model, int userId);
}

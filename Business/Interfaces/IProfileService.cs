using Domain.Models;

namespace Business.Interfaces;

public interface IProfileService
{
    Task<UserProfileModel?> GetAsync(string userId);
    Task<CreateProfileResult> CreateAsync(CreateProfileModel model, string userId);
    Task<IEnumerable<UserProfileModel>> GetAllAsync();
}

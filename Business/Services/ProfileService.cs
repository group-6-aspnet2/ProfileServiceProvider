using Business.Interfaces;
using Domain.Models;

namespace Business.Services;

public class ProfileService : IProfileService
{
    public Task<UserProfileModel> CreateAsync(CreateProfileModel model, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfileModel> GetAsync(int userId)
    {
        throw new NotImplementedException();
    }
}

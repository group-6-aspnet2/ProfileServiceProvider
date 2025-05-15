using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProfileService(DataContext context) : IProfileService
{
    private readonly DataContext _context = context;

    public async Task<UserProfileModel> CreateAsync(CreateProfileModel model, int userId)
    {
        var entity = new UserProfileEntity
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            PostalCode = model.PostalCode,
            Role = model.Role
        };

        _context.UserProfiles.Add(entity);
        await _context.SaveChangesAsync();

        return new UserProfileModel
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Address = entity.Address,
            PostalCode = entity.PostalCode,
            Role = entity.Role
        };
    }

    public async Task<UserProfileModel?> GetAsync(int userId)
    {
        var entity = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

        if (entity == null) 
            return null;

        return new UserProfileModel
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Address = entity.Address,
            PostalCode = entity.PostalCode,
            Role = entity.Role
        };
    }
}

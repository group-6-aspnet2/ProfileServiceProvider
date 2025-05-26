using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;

public class ProfileService(DataContext context) : IProfileService 
{
    private readonly DataContext _context = context;

    public async Task<CreateProfileResult> CreateAsync(CreateProfileModel model, string userId)
    {
        var entity = new UserProfileEntity
        {
            UserId = userId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            PostalCode = model.PostalCode,
            Role = model.Role
        };

        _context.UserProfiles.Add(entity);
        await _context.SaveChangesAsync();

        return new CreateProfileResult
        {
            Succeeded = true,
            Message = "Profile created successfully.",
            Profile = new UserProfileModel
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Address = entity.Address,
                PostalCode = entity.PostalCode,
                Role = entity.Role
            }
        };
    }

    public async Task<UserProfileModel?> GetAsync(string userId)
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

    public async Task<IEnumerable<UserProfileModel>> GetAllAsync()
    {
        try
        {
            var entities = await _context.UserProfiles.ToListAsync();

            var models= entities.Select(e => new UserProfileModel
            {
                UserId = e.UserId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Address = e.Address,
                PostalCode = e.PostalCode,
                Role = e.Role
            });
            return models;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}
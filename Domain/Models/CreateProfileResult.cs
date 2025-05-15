

namespace Domain.Models;

public class CreateProfileResult
{
    public bool Succeeded { get; set; } 
    public string? Message { get; set; } 
    public UserProfileModel? Profile { get; set; }
}

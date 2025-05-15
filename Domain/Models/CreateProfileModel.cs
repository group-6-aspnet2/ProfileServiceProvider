namespace Domain.Models;

public class CreateProfileModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int PostalCode { get; set; }
    public string Role { get; set; } = null!;
}
namespace Data.Entities;

public class UserProfileEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Address { get; set; } = null!;
    public int PostalCode { get; set; }
    public string Role { get; set; } = null!;
}

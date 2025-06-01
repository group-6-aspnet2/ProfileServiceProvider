using Moq;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Business.Services;

//Tagit hjälp av chatgpt
public class ProfileServiceSimpleTests
{
    [Fact]
    public async Task GetAsync_ReturnsUserProfile_WhenUserExists()
    {
        // Arrange
        var userId = "user-1";
        var data = new List<UserProfileEntity>
        {
            new() { UserId = userId, FirstName = "Test", LastName = "User", Address = "Addr", PostalCode = 12345, Role = "User" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<UserProfileEntity>>();
        mockSet.As<IQueryable<UserProfileEntity>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<UserProfileEntity>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<UserProfileEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<UserProfileEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
        mockContext.Setup(c => c.UserProfiles).Returns(mockSet.Object);

        var service = new ProfileService(mockContext.Object);

        // Act
        var result = await service.GetAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.FirstName);
    }
}

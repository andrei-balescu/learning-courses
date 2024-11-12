using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Playstore.Auth.Service;
using Playstore.Auth.Service.Data;

namespace Playstore.Auth.Test.Services;

/// <summary>see doc https://stackoverflow.com/questions/54219742/mocking-ef-core-dbcontext-and-dbset</summary>
[TestClass]
public class UserServiceTests
{
    DbContextOptions<AppDbContext> _options;
    private AppDbContext _appDbContext;

    private UserService _userService;

    public UserServiceTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Users")
            .Options;
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _appDbContext = new AppDbContext(_options);
        _userService = new UserService(_appDbContext);
    }

    [TestMethod]
    public void GetUser_AppliesQuery()
    {
        // Arrange
        string expectedUserName = "expectedUserName";
        IdentityUser expectedUser = new() { UserName = expectedUserName };

        _appDbContext.Users.Add(expectedUser);
        _appDbContext.SaveChanges();

        // Act
        IdentityUser actualUser = _userService.GetUser(u => u.UserName == expectedUserName);

        // Assert
        Assert.AreEqual(expectedUser.UserName, actualUser.UserName);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _appDbContext.Dispose();
    }
}
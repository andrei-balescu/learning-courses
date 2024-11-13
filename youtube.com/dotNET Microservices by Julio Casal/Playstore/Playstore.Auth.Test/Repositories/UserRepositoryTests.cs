using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Playstore.Auth.Respositories;

namespace Playstore.Auth.Test.Repositories;

/// <summary>see doc https://stackoverflow.com/questions/54219742/mocking-ef-core-dbcontext-and-dbset</summary>
[TestClass]
public class UserRepositoryTests
{
    DbContextOptions<AppDbContext> _options;
    private AppDbContext _appDbContext;

    private UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Users")
            .Options;
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _appDbContext = new AppDbContext(_options);
        _userRepository = new UserRepository(_appDbContext);
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
        IdentityUser actualUser = _userRepository.GetUser(u => u.UserName == expectedUserName);

        // Assert
        Assert.AreEqual(expectedUser.UserName, actualUser.UserName);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _appDbContext.Dispose();
    }
}
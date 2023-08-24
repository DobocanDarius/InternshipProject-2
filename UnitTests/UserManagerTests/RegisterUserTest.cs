using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace UnitTests.UserManagerTests;

[TestClass]
public class RegisterUserTest
{
    private UserManager _userManager;
    private  Project2Context _dbContext;
    private  PasswordHasher _passwordHasher;

    [TestInitialize]
    public void Setup()
    {   
        _dbContext = new Project2Context();
        _passwordHasher = new PasswordHasher();
        _userManager = new UserManager(_dbContext,_passwordHasher);
    }
    [TestMethod]
    public async Task CreateUserValidRequest()
    {
        //arrange
        var request = new CreateUserRequest("darius", "darius", "darius@d.com", "developer");

        //act
        CreateUserResponse result = await _userManager.Create(request);
        
        //assert
        Assert.AreEqual("Registration successful", result.Message);
    }

    [TestMethod]
    public async Task UserExistsValidRequest()
    {
        //arrange
        var request = new CreateUserRequest("darius", "darius", "darius@d.com", "developer");

        //act
        CreateUserResponse result = await _userManager.Create(request);

        //assert
        Assert.AreEqual("User with this email already exists", result.Message);
    }
}

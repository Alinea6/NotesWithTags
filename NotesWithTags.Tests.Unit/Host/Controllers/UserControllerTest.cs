using FluentAssertions;
using Moq;
using NotesWithTags.API.Controllers;
using NotesWithTags.API.Models;
using NotesWithTags.Services.App.Int;
using NotesWithTags.Tests.Unit.Helpers;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Host.Controllers;

public class UserControllerTest
{
    private UserController _sut;
    private Mock<IUserService> _userServiceMock;

    [SetUp]
    public void SetUp()
    {
        _userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
        _sut = new UserController(_userServiceMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _userServiceMock.VerifyAll();
    }

    [Test]
    public async Task Login_should_map_request_to_input_call_user_service_and_return_user_id()
    {
        // arrange
        var request = new LoginRequest
        {
            Login = "fake-login",
            Password = "fake-password"
        };
        var input = new LoginInput
        {
            Login = "fake-login",
            Password = "fake-password"
        };

        _userServiceMock.Setup(x => x.Login(MoqHandler.IsEquivalentTo(input), It.IsAny<CancellationToken>()))
            .ReturnsAsync("fake-user-id");
        
        // act
        var result = await _sut.Login(request);
        
        // assert
        result.Should().Be("fake-user-id");
    }

    [Test]
    public async Task Register_should_map_request_to_input_call_user_service_and_return_token()
    {
        // arrange
        var request = new RegisterRequest
        {
            Login = "fake-login",
            Password = "fake-password"
        };
        var input = new RegisterInput
        {
            Login = "fake-login",
            Password = "fake-password"
        };

        _userServiceMock.Setup(x => x.Register(MoqHandler.IsEquivalentTo(input), It.IsAny<CancellationToken>()))
            .ReturnsAsync("fake-token");
        
        // act
        var result = await _sut.Register(request);
        
        // assert
        result.Should().Be("fake-token");
    }
}
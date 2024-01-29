using FluentAssertions;
using Moq;
using NotesWithTags.Services.App;
using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Services;

public class UserServiceTest
{
    private UserService _sut;
    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        _sut = new UserService(_userRepositoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _userRepositoryMock.VerifyAll();
    }

    [Test]
    public async Task Login_should_call_user_repository_and_return_token()
    {
        // arrange
        var input = new LoginInput();

        _userRepositoryMock.Setup(x => x.Login(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync("fake-token");
        
        // act
        var result = await _sut.Login(input);
        
        // assert
        result.Should().Be("fake-token");
    }

    [Test]
    public async Task Register_should_call_user_repository_and_return_user_id()
    {
        // arrange
        var input = new RegisterInput();

        _userRepositoryMock.Setup(x => x.Register(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync("fake-id");
        
        // act
        var result = await _sut.Register(input);
        
        // assert
        result.Should().Be("fake-id");
    }
}
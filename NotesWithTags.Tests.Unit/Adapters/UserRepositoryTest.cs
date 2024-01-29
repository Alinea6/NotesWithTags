using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Moq;
using NotesWithTags.Adapters.Data;
using NotesWithTags.Adapters.Data.Models;
using NotesWithTags.Adapters.Data.Repositories;
using NotesWithTags.Services.App.Int;
using NotesWithTags.Tests.Unit.Helpers;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Adapters;

public class UserRepositoryTest
{
    private UserRepository _sut;
    private DataContext _context;
    private Mock<IJsonWebTokenProvider> _jwtProviderMock;

    [SetUp]
    public void SetUp()
    {
        _context = FakeDbContextProvider.GetFakeDbContext();
        _jwtProviderMock = new Mock<IJsonWebTokenProvider>(MockBehavior.Strict);
        _sut = new UserRepository(_context, _jwtProviderMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _jwtProviderMock.VerifyAll();
    }

    [Test]
    public async Task Register_should_create_user_and_return_user_id()
    {
        // arrange
        var input = new RegisterInput
        {
            Login = "fake-login-3",
            Password = "fake-password-3"
        };
        
        // act
        var result = await _sut.Register(input);
        
        // assert
        _context.Users.Count().Should().Be(3);
    }

    [Test]
    public async Task Login_should_find_user_with_login_and_password_generate_token_and_return_it()
    {
        // arrange
        var user = new User
        {
            Id = "fake-id-1",
            Login = "fake-login-1",
            Password = "fake-password-1"
        };
        var token = new JwtSecurityToken();

        _jwtProviderMock.Setup(x => x.GenerateToken(MoqHandler.IsEquivalentTo(user)))
            .Returns(token);
        
        // act
        var result = await _sut.Login(new LoginInput
        {
            Login = "fake-login-1",
            Password = "fake-password-1"
        });
        
        // assert
        result.Should().NotBeEmpty();
    }
}
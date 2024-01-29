using Microsoft.AspNetCore.Mvc;
using NotesWithTags.API.Models;
using NotesWithTags.Services.App.Int;
using LoginRequest = NotesWithTags.API.Models.LoginRequest;

namespace NotesWithTags.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public Task<string> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        return _userService.Login(new LoginInput
        {
            Login = request.Login,
            Password = request.Password
        }, cancellationToken);
    }

    [HttpPost("register")]
    public Task<string> Register(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        return _userService.Register(new RegisterInput
        {
            Login = request.Login,
            Password = request.Password
        }, cancellationToken);
    }
}
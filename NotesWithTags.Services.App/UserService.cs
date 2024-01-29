using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<string> Login(LoginInput input, CancellationToken cancellationToken = default)
    {
        return _userRepository.Login(input, cancellationToken);
    }

    public Task<string> Register(RegisterInput input, CancellationToken cancellationToken = default)
    {
        return _userRepository.Register(input, cancellationToken);
    }
}
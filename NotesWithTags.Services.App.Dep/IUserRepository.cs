using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App.Dep;

public interface IUserRepository
{
    Task<string> Login(LoginInput input, CancellationToken cancellationToken = default);
    Task<string> Register(RegisterInput input, CancellationToken cancellationToken = default);
}
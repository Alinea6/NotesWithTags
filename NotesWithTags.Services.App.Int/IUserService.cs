namespace NotesWithTags.Services.App.Int;

public interface IUserService
{
    Task<string> Login(LoginInput input, CancellationToken cancellationToken = default);
    Task<string> Register(RegisterInput input, CancellationToken cancellationToken = default);
}
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Adapters.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;
    private readonly IJsonWebTokenProvider _jwtProvider;

    public UserRepository(DataContext dataContext, IJsonWebTokenProvider jwtProvider)
    {
        _dataContext = dataContext;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Login(LoginInput input, CancellationToken cancellationToken = default)
    {
        var user = await _dataContext.Users
            .Where(x => x.Login == input.Login && x.Password == input.Password)
            .SingleOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new Exception("Incorrect login or password");
        }

        JwtSecurityToken jwtSecurityToken = _jwtProvider.GenerateToken(user);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task<string> Register(RegisterInput input, CancellationToken cancellationToken = default)
    {
        var user = new Models.User
        {
            Id = Guid.NewGuid().ToString(),
            Login = input.Login,
            Password = input.Password
        };

        _dataContext.Add(user);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return user.Id;
    }
}
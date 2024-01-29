using System.Security.Claims;

namespace NotesWithTags.API.Controllers;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected string? GetUserId()
    {
        return User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
    }
}
namespace NotesWithTags.API.Controllers;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected string GetUserId()
    {
        // TODO: replace with jwt implementation
        throw new NotImplementedException();
    }
}
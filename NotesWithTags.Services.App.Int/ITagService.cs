namespace NotesWithTags.Services.App.Int;

public interface ITagService
{
    IEnumerable<string> AddTags(string noteText);
}
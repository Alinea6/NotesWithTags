namespace NotesWithTags.Services.App.Int;

public class NoteAdd
{
    public string? UserId { get; set; }
    public string NoteText { get; set; }
    public IEnumerable<string> Tags { get; set; }
}
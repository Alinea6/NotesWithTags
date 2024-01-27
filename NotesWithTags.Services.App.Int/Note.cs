namespace NotesWithTags.Services.App.Int;

public class Note
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string NoteText { get; set; }
    public IEnumerable<string> Tags { get; set; }
}
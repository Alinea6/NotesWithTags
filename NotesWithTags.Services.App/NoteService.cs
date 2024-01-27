using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App;

public class NoteService : INoteService
{
    public Task<IEnumerable<Note>> GetAllNotes(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Note> GetNoteById(string noteId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Note> Add(NoteAdd input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Note> Update(NoteUpdate input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string noteId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
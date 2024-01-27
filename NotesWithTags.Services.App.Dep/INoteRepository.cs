using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App.Dep;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotes(string userId, CancellationToken cancellationToken = default);
    Task<Note> GetNoteById(string noteId, string userId, CancellationToken cancellationToken = default);
    Task<Note> Add(NoteAdd input, CancellationToken cancellationToken = default);
    Task<Note> Update(NoteUpdate input, CancellationToken cancellationToken = default);
    Task Delete(string noteId, string userId, CancellationToken cancellationToken = default);
}
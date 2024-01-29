using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App;

public class NoteService : INoteService
{
    private readonly ITagService _tagService;
    private readonly INoteRepository _noteRepository;

    public NoteService(ITagService tagService, INoteRepository noteRepository)
    {
        _tagService = tagService;
        _noteRepository = noteRepository;
    }
    
    public Task<IEnumerable<Note>> GetAllNotes(string? userId, CancellationToken cancellationToken = default)
    {
        return _noteRepository.GetAllNotes(userId, cancellationToken);
    }

    public Task<Note> GetNoteById(string noteId, string? userId, CancellationToken cancellationToken = default)
    {
        return _noteRepository.GetNoteById(noteId, userId, cancellationToken);
    }

    public async Task<Note> Add(NoteAdd input, CancellationToken cancellationToken = default)
    {
        input.Tags = _tagService.AddTags(input.NoteText);
        return await _noteRepository.Add(input, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Note> Update(NoteUpdate input, CancellationToken cancellationToken = default)
    {
        input.Tags = _tagService.AddTags(input.NoteText);
        return await _noteRepository.Update(input, cancellationToken).ConfigureAwait(false);
    }

    public Task Delete(string noteId, string? userId, CancellationToken cancellationToken = default)
    {
        return _noteRepository.Delete(noteId, userId, cancellationToken);
    }
}
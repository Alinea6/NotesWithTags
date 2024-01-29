using Microsoft.EntityFrameworkCore;
using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Adapters.Data.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly DataContext _dataContext;

    public NoteRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Note>> GetAllNotes(string? userId, CancellationToken cancellationToken = default)
    {
        
        var notes = await _dataContext.Notes
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return notes.Select(MapToNote);
    }

    public async Task<Note> GetNoteById(string noteId, string? userId, CancellationToken cancellationToken = default)
    {
        var note = await _dataContext.Notes
            .Where(x => x.Id == noteId && x.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (note == null)
        {
            throw new Exception($"Note with id {noteId} for user {userId} was not found");
        }

        return MapToNote(note);
    }

    public async Task<Note> Add(NoteAdd input, CancellationToken cancellationToken = default)
    {
        var note = new Models.Note
        {
            Id = Guid.NewGuid().ToString(),
            UserId = input.UserId,
            NoteText = input.NoteText,
            Tags = input.Tags.ToList()
        };

        _dataContext.Add(note);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return MapToNote(note);
    }

    public async Task<Note> Update(NoteUpdate input, CancellationToken cancellationToken = default)
    {
        var note = await _dataContext.Notes
            .Where(x => x.Id == input.Id && x.UserId == input.UserId)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (note == null)
        {
            throw new Exception($"Note with id {input.Id} for user {input.UserId} was not found");
        }

        note.NoteText = input.NoteText;
        note.Tags = input.Tags.ToList();

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return MapToNote(note);
    }

    public async Task Delete(string noteId, string? userId, CancellationToken cancellationToken = default)
    {
        var note = await _dataContext.Notes
            .Where(x => x.Id == noteId && x.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (note == null)
        {
            throw new Exception($"Note with id {noteId} for user {userId} was not found");
        }

        _dataContext.Remove(note);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    private static Note MapToNote(Models.Note input)
    {
        return new Note
        {
            Id = input.Id,
            UserId = input.UserId,
            NoteText = input.NoteText,
            Tags = input.Tags
        };
    }
}
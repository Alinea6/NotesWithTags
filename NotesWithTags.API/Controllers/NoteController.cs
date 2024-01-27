using Microsoft.AspNetCore.Mvc;
using NotesWithTags.API.Models;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public Task<IEnumerable<Note>> GetAllNotes(CancellationToken cancellationToken = default)
    {
        return _noteService.GetAllNotes(GetUserId(), cancellationToken);
    }

    [HttpGet("{noteId}")]
    public Task<Note> GetNoteById(string noteId, CancellationToken cancellationToken = default)
    {
        return _noteService.GetNoteById(noteId, GetUserId(), cancellationToken);
    }
    
    [HttpPost]
    public Task<Note> AddNote(NoteUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return _noteService.Add(new NoteAdd
        {
            NoteText = request.NoteText,
            UserId = GetUserId()
        }, cancellationToken);
    }

    [HttpPut("{noteId}")]
    public Task<Note> UpdateNote(string noteId, NoteUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return _noteService.Update(new NoteUpdate
        {
            Id = noteId,
            NoteText = request.NoteText,
            UserId = GetUserId()
        }, cancellationToken);
    } 

    [HttpDelete("{noteId}")]
    public Task DeleteNote(string noteId, CancellationToken cancellationToken = default)
    {
        return _noteService.Delete(noteId, GetUserId(), cancellationToken);
    }
}
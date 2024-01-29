using Microsoft.EntityFrameworkCore;
using NotesWithTags.Adapters.Data;
using NotesWithTags.Adapters.Data.Models;

namespace NotesWithTags.Tests.Unit.Adapters;

public static class FakeDbContextProvider
{
    public static DataContext GetFakeDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"FakeDatabase_{Guid.NewGuid()}")
            .Options;
        var context = new DataContext(options);

        var notes = GenerateNotes();

        context.Notes.AddRange(notes);
        context.SaveChangesAsync();

        return context;
    }

    private static Note[] GenerateNotes()
    {
        return
        [
            new Note
            {
                Id = "fake-id-1",
                UserId = "fake-user-id-1",
                NoteText = "fake-note-text-1",
                Tags = ["fake-tag-1", "fake-tag-2"]
            },
            new Note
            {
                Id = "fake-id-2",
                UserId = "fake-user-id-1",
                NoteText = "fake-note-text-2",
                Tags = ["fake-tag-1"]
            },
            new Note
            {
                Id = "fake-id-3",
                UserId = "fake-user-id-2",
                NoteText = "fake-note-text-3",
                Tags = ["fake-tag-2"]
            }
        ];
    }
}
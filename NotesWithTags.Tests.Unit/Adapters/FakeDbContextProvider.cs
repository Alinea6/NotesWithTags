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
        var users = GenerateUsers();

        context.Notes.AddRange(notes);
        context.Users.AddRange(users);
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

    private static User[] GenerateUsers()
    {
        return
        [
            new User
            {
                Id = "fake-id-1",
                Login = "fake-login-1",
                Password = "fake-password-1"
            },
            new User
            {
                Id = "fake-id-2",
                Login = "fake-login-2",
                Password = "fake-password-2"
            }
        ];
    }
}
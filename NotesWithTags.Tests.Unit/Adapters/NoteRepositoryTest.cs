using FluentAssertions;
using NotesWithTags.Adapters.Data;
using NotesWithTags.Adapters.Data.Repositories;
using NotesWithTags.Services.App.Int;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Adapters;

public class NoteRepositoryTest
{
    private NoteRepository _sut;
    private DataContext _context;

    [SetUp]
    public void SetUp()
    {
        _context = FakeDbContextProvider.GetFakeDbContext();
        _sut = new NoteRepository(_context);
    }

    [Test]
    public async Task GetAllNotes_should_get_all_notes_with_specific_user_id()
    {
        // arrange
        var expected = new List<Note>
        {
            new()
            {
                Id = "fake-id-1",
                UserId = "fake-user-id-1",
                NoteText = "fake-note-text-1",
                Tags = new List<string> { "fake-tag-1", "fake-tag-2" }
            },
            new()
            {
                Id = "fake-id-2",
                UserId = "fake-user-id-1",
                NoteText = "fake-note-text-2",
                Tags = new List<string> { "fake-tag-1" }
            }
        };
        
        // act
        var result = await _sut.GetAllNotes("fake-user-id-1");
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetAllNotes_should_return_empty_list_if_user_has_no_notes()
    {
        // arrange
        var expected = new List<Note>();
        
        // act
        var result = await _sut.GetAllNotes("fake-user-id-99");
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetNoteById_should_return_correct_note()
    {
        // arrange
        var expected = new Note
        {
            Id = "fake-id-3",
            UserId = "fake-user-id-2",
            NoteText = "fake-note-text-3",
            Tags = ["fake-tag-2"]
        };
        
        // act
        var result = await _sut.GetNoteById("fake-id-3", "fake-user-id-2");
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetNoteById_should_throw_error_when_note_was_not_found()
    {
        // act
        var act = () => _sut.GetNoteById("fake-id-99", "fake-user-id-99");
        
        // assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Note with id fake-id-99 for user fake-user-id-99 was not found");
    }

    [Test]
    public async Task Add_should_create_note_and_return_it()
    {
        // arrange
        var input = new NoteAdd
        {
            UserId = "fake-user-id",
            NoteText = "fake-note-text",
            Tags = new List<string> { "fake-tag-1", "fake-tag-2" }
        };
        
        // act
        var result = await _sut.Add(input);
        
        // assert
        result.Should().BeEquivalentTo(new Note
        {
            Id = result.Id,
            UserId = "fake-user-id",
            NoteText = "fake-note-text",
            Tags = new List<string> { "fake-tag-1", "fake-tag-2" }
        });
        _context.Notes.Count().Should().Be(4);
    }

    [Test]
    public async Task Update_should_update_note_and_return_it()
    {
        // arrange
        var input = new NoteUpdate
        {
            Id = "fake-id-3",
            UserId = "fake-user-id-2",
            NoteText = "updated-note",
            Tags = new List<string> { "updated-tag" }
        };
        var expected = new Note
        {
            Id = "fake-id-3",
            UserId = "fake-user-id-2",
            NoteText = "updated-note",
            Tags = new List<string> { "updated-tag" }
        };
        
        // act
        var result = await _sut.Update(input);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task Update_should_throw_error_when_note_does_not_exist()
    {
        // arrange
        var input = new NoteUpdate
        {
            Id = "fake-id-99",
            UserId = "fake-user-id-2",
            NoteText = "updated-note",
            Tags = new List<string> { "updated-tag" }
        };
        
        // act
        var act = () => _sut.Update(input);
         
        // assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Note with id fake-id-99 for user fake-user-id-2 was not found");
    }

    [Test]
    public async Task Delete_should_remove_note_and_not_throw_error()
    {
        // act
        var act = () => _sut.Delete("fake-id-1", "fake-user-id-1");
        
        // assert
        await act.Should().NotThrowAsync();
        _context.Notes.Count().Should().Be(2);
    }

    [Test]
    public async Task Delete_should_throw_error_when_note_does_not_exist()
    {
        // act
        var act = () => _sut.Delete("fake-id-99", "fake-user-id-99");
        
        // assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Note with id fake-id-99 for user fake-user-id-99 was not found");
        _context.Notes.Count().Should().Be(3);
    }
}
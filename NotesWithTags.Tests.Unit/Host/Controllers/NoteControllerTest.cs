using FluentAssertions;
using Moq;
using NotesWithTags.API.Controllers;
using NotesWithTags.API.Models;
using NotesWithTags.Services.App.Int;
using NotesWithTags.Tests.Unit.Helpers;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Host.Controllers;

public class NoteControllerTest
{
    private NoteController _sut;
    private Mock<INoteService> _noteServiceMock;

    [SetUp]
    public void SetUp()
    {
        _noteServiceMock = new Mock<INoteService>(MockBehavior.Strict);
        _sut = new NoteController(_noteServiceMock.Object);
        
        //TODO: add helper for getting id from jwt
    }

    [TearDown]
    public void TearDown()
    {
        _noteServiceMock.VerifyAll();
    }

    [Test]
    public async Task GetAllNotes_should_call_note_service_and_return_list_of_notes()
    {
        // arrange
        var expected = new List<Note>
        {
            new Note
            {
                Id = "fake-note-id",
                NoteText = "fake-note-text"
            }
        };

        _noteServiceMock.Setup(x => x.GetAllNotes("fake-user-id", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.GetAllNotes();
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetNoteById_should_call_note_service_and_return_note()
    {
        // arrange
        var expected = new Note();

        _noteServiceMock.Setup(x => x.GetNoteById("fake-note-id", "fake-user-id", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.GetNoteById("fake-note-id");
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task AddNote_should_call_note_service_and_return_created_note()
    {
        // arrange
        var request = new NoteUpdateRequest
        {
            NoteText = "fake-text"
        };
        var input = new NoteAdd
        {
            NoteText = "fake-text",
            UserId = "fake-user-id"
        };
        var expected = new Note();

        _noteServiceMock.Setup(x => x.Add(MoqHandler.IsEquivalentTo(input), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.AddNote(request);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task UpdateNote_should_call_note_service_and_return_updated_note()
    {
        // arrange
        var request = new NoteUpdateRequest
        {
            NoteText = "fake-text"
        };

        var input = new NoteUpdate
        {
            Id = "fake-note-id",
            NoteText = "fake-text",
            UserId = "fake-user-id"
        };

        var expected = new Note();

        _noteServiceMock.Setup(x => x.Update(MoqHandler.IsEquivalentTo(input), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.UpdateNote("fake-note-id", request);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task DeleteNote_should_call_note_service_to_delete_note()
    {
        // arrange
        var noteId = "fake-note-id";
        var userId = "fake-user-id";

        _noteServiceMock.Setup(x => x.Delete(noteId, userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // act
        var act = () => _sut.DeleteNote(noteId);
        
        // arrange
        await act.Should().NotThrowAsync();
    }
}
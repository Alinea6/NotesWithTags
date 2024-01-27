using FluentAssertions;
using Moq;
using NotesWithTags.Services.App;
using NotesWithTags.Services.App.Dep;
using NotesWithTags.Services.App.Int;
using NotesWithTags.Tests.Unit.Helpers;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Services;

public class NoteServiceTest
{
    private NoteService _sut;
    private Mock<ITagService> _tagServiceMock;
    private Mock<INoteRepository> _noteRepositoryMock;

    [SetUp]
    public void SetUp()
    {
        _tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
        _noteRepositoryMock = new Mock<INoteRepository>(MockBehavior.Strict);
        _sut = new NoteService(_tagServiceMock.Object, _noteRepositoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _tagServiceMock.VerifyAll();
        _noteRepositoryMock.VerifyAll();
    }

    [Test]
    public async Task GetAllNotes_should_call_repository_and_return_list_of_notes()
    {
        // arrange
        var userId = "fake-user-id";
        var expected = new List<Note>
        {
            new Note
            {
                Id = "fake-user-id",
                NoteText = "fake-note-text"
            }
        };

        _noteRepositoryMock.Setup(x => x.GetAllNotes(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.GetAllNotes(userId);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetNoteById_should_call_repository_and_return_note()
    {
        // arrange
        var userId = "fake-user-id";
        var noteId = "fake-note-id";
        var expected = new Note();

        _noteRepositoryMock.Setup(x => x.GetNoteById(noteId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.GetNoteById(noteId, userId);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task Add_should_call_tag_service_for_tags_then_call_repository_and_return_created_note()
    {
        // arrange
        var inputBeforeTags = new NoteAdd
        {
            NoteText = "fake-note-text",
            UserId = "fake-user-id"
        };
        var tags = new List<string> { "fake-tag-1", "fake-tag-2" };
        var inputWithTags = new NoteAdd
        {
            UserId = "fake-user-id",
            NoteText = "fake-note-text",
            Tags = tags
        };
        var expected = new Note();

        _tagServiceMock.Setup(x => x.AddTags(inputBeforeTags.NoteText))
            .Returns(tags);

        _noteRepositoryMock.Setup(x => x.Add(MoqHandler.IsEquivalentTo(inputWithTags), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.Add(inputBeforeTags);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task Update_should_call_tag_service_for_tags_then_call_repository_and_return_updated_note()
    {
        // arrange
        var inputBeforeTags = new NoteUpdate
        {
            NoteText = "fake-note-text",
            UserId = "fake-user-id"
        };
        var tags = new List<string> { "fake-tag-1", "fake-tag-2" };
        var inputWithTags = new NoteUpdate
        {
            UserId = "fake-user-id",
            NoteText = "fake-note-text",
            Tags = tags
        };
        var expected = new Note();

        _tagServiceMock.Setup(x => x.AddTags(inputBeforeTags.NoteText))
            .Returns(tags);

        _noteRepositoryMock.Setup(x => x.Update(MoqHandler.IsEquivalentTo(inputWithTags), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _sut.Update(inputBeforeTags);
        
        // assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task Delete_should_call_repository_to_delete_specific_note()
    {
        // arrange
        var noteId = "fake-note-id";
        var userId = "fake-user-id";
        
        _noteRepositoryMock.Setup(x => x.Delete(noteId, userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        // act
        var act = () => _sut.Delete(noteId, userId);
        
        // assert
        await act.Should().NotThrowAsync();
    }
}
using FluentAssertions;
using NotesWithTags.Services.App;
using NUnit.Framework;

namespace NotesWithTags.Tests.Unit.Services;

public class TagServiceTest
{
    private TagService _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new TagService();
    }

    [TestCase("email@example.com", Description = "note with only email")]
    [TestCase("one two three email@example.com one", Description = "note with email and other words before and after")]
    [TestCase(" email@example.com  ", Description = "note with email and whitespaces")]
    public void AddTags_should_return_only_email_tag(string note)
    {
        // arrange
        var expected = new List<string> { "EMAIL" };
        
        // act
        var result = _sut.AddTags(note);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestCase("+48123456789", Description = "only international phone number")]
    [TestCase("123456789", Description = "only country number with nine digits")]
    public void AddTags_should_return_only_phone_tags(string note)
    {
        // arrange
        var expected = new List<string> { "PHONE" };
        
        // act
        var result = _sut.AddTags(note);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void AddTags_return_phone_and_email_tag()
    {
        // arrange
        var note = "email@example.com and 48123456789";
        var expected = new List<string> { "PHONE", "EMAIL" };
        
        // act
        var result = _sut.AddTags(note);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestCase("note", Description = "note without email and phone")]
    [TestCase("test email @example.com", Description = "email with whitespace")]
    [TestCase("test email@example..com", Description = "email with multiple dots next to domain")]
    [TestCase("test 12345678", Description = "phone with incorrect number of digits")]
    [TestCase("test 123-456-789", Description = "phone number with dashes")]
    [TestCase("test 123 456 789", Description = "Phone number with whitespaces")]
    public void AddTags_returns_empty_list_of_tags(string note)
    {
        // arrange
        var expected = new List<string>();
        
        // act
        var result = _sut.AddTags(note);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
}
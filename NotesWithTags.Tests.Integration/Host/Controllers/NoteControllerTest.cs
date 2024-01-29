using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NotesWithTags.API.Models;
using NotesWithTags.Services.App.Int;
using NUnit.Framework;

namespace NotesWithTags.Tests.Integration.Host.Controllers;

public partial class NoteControllerTest
{
    private WebApplicationFactory<Program> _clientApiFactory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void SetUp()
    {
        _clientApiFactory = ClientApiFactoryProvider.Create((context, services) =>
        {
        });
        _client = _clientApiFactory.CreateClient();
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiNzZlOWQ3NDktOTQ0Yy00OGM3LTg1NDUtOTAzMTNhZGU1ZTc4IiwiZXhwIjoxODAxMjU5NTYyLCJpc3MiOiJOb3Rlc1dpdGhUYWdzLkFQSSIsImF1ZCI6Ik5vdGVzV2l0aFRhZ3NVc2VycyJ9.2nGzD3k5p-FwIWzQQxaHnnG5nw7J1SkYOmd1aO5U50A";
    
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Test]
    public async Task NoteController_should_add_get_update_and_delete_note()
    {
        var noteId = await AddNoteShouldReturnCreatedNote();
        await GetAllNotesShouldReturnNotes();
        await GetNoteByIdShouldReturnNote(noteId);
        await UpdateShouldReturnNote(noteId);
        await DeleteShouldNotThrowAsync(noteId);
    }

    private async Task<string> AddNoteShouldReturnCreatedNote()
    {
        var request = new NoteUpdateRequest
        {
            NoteText = "new-note"
        };

        var json = JsonConvert.SerializeObject(request);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/note", data);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Note>(responseString);

        result.Should().NotBeNull();
        return result.Id;
    }

    private async Task GetAllNotesShouldReturnNotes()
    {
        var response = await _client.GetAsync("api/note");
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<Note>>(responseString);

        result.Should().NotBeNull();
    }

    private async Task GetNoteByIdShouldReturnNote(string noteId)
    {
        var response = await _client.GetAsync($"api/note/{noteId}");
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Note>(responseString);

        result.Should().NotBeNull();
    }

    private async Task UpdateShouldReturnNote(string noteId)
    {
        var request = new NoteUpdateRequest
        {
            NoteText = "example@example.com"
        };

        var json = JsonConvert.SerializeObject(request);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"api/note/{noteId}", data);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Note>(responseString);

        result.Should().NotBeNull();
    }

    private async Task DeleteShouldNotThrowAsync(string noteId)
    {
        var act = () => _client.DeleteAsync($"api/note/{noteId}");

        await act.Should().NotThrowAsync();
    }
}
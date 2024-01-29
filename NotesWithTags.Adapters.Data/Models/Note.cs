using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesWithTags.Adapters.Data.Models;

[Table("Note")]
public class Note
{
    [Key]
    public string Id { get; set; }
    
    [Required]
    public string? UserId { get; set; }
    
    public string NoteText { get; set; }
    
    public List<string> Tags { get; set; }
}
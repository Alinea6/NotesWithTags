using System.ComponentModel.DataAnnotations;

namespace NotesWithTags.Adapters.Data.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
}
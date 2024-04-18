using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotnetService.Infrastructure.Models;

[Table("Users")]
public class User
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    public string Username { get; set; }

    public string? Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public Roles Roles { get; set; }
}

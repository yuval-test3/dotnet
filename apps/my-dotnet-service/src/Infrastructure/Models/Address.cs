using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotnetService.Infrastructure.Models;

[Table("Addresses")]
public class Address
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Address_1 { get; set; }

    public string? Address_2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int? Zip { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}

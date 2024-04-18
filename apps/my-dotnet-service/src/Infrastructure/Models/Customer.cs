using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotnetService.Infrastructure.Models;

[Table("Customers")]
public class Customer
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public string AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public Address? Address { get; set; }
}

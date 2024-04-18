using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotnetService.Infrastructure.Models;

[Table("Products")]
public class Product
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public decimal? ItemPrice { get; set; }

    public string? Description { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

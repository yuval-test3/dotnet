using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDotnetService.Infrastructure.Models;

[Table("Orders")]
public class Order
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public int? TotalPrice { get; set; }

    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }

    public string ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
}

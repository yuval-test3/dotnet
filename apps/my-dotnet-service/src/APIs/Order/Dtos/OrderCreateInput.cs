namespace MyDotnetService.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }
    public long? Quantity { get; set; }
    public decimal? Discount { get; set; }
    public long? TotalPrice { get; set; }
    public CustomerDto CustomerId { get; set; }
    public ProductDto ProductId { get; set; }
}

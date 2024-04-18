namespace MyDotnetService.APIs.Dtos;

public class ProductWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public decimal? ItemPrice { get; set; }
    public ICollection<OrderDto>? Orders { get; set; }
}

namespace MyDotnetService.APIs.Dtos;

public class CustomerWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public ICollection<OrderDto>? Orders { get; set; }
    public AddressDto AddressId { get; set; }
}

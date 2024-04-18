namespace MyDotnetService.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public ICollection<OrderDto>? Orders { get; set; }
    public AddressDto AddressId { get; set; }
    public string? Phone_2 { get; set; }
}

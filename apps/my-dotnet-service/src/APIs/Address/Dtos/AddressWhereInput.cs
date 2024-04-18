namespace MyDotnetService.APIs.Dtos;

public class AddressWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? Address_1 { get; set; }
    public string? Address_2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public long? Zip { get; set; }
    public ICollection<CustomerDto>? Customers { get; set; }
}

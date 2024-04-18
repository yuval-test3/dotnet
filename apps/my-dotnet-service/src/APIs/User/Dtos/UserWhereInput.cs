namespace MyDotnetService.APIs.Dtos;

public class UserWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

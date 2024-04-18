using MyDotnetService.APIs.Dtos;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs.Extensions;

public static class CitiesExtensions
{
    public static CityDto ToDto(this City model)
    {
        return new CityDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
        };
    }
}

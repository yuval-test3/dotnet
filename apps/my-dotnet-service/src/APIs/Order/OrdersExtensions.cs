using MyDotnetService.APIs.Dtos;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs.Extensions;

public static class OrdersExtensions
{
    public static OrderDto ToDto(this Order model)
    {
        return new OrderDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Quantity = model.Quantity,
            Discount = model.Discount,
            TotalPrice = model.TotalPrice,
            Customers = model.Customers.Select(x => x.ToDto()).ToList(),
            Products = model.Products.Select(x => x.ToDto()).ToList(),
        };
    }
}

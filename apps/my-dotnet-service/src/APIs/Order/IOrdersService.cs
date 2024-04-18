using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface IOrdersService
{
    public Task<Order> CreateOrder(OrderCreateInput input);
    public Task DeleteOrder(string id);
    public Task<IEnumerable<Order>> Orders();
    public Task<IEnumerable<Customer>> Customers(string id);
    public Task<IEnumerable<Product>> Products(string id);
    public Task<IEnumerable<Customer>> Customers(string id);
    public Task<IEnumerable<Product>> Products(string id);
    public Task UpdateOrder(string id, Order dto);
}

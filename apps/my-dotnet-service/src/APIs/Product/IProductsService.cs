using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface IProductsService
{
    public Task<Product> CreateProduct(ProductCreateInput input);
    public Task DeleteProduct(string id);
    public Task<IEnumerable<Product>> Products();
    public Task ConnectOrder(string id, [Required] string OrderId);
    public Task DisconnectOrder(string id, [Required] string OrderId);
    public Task<IEnumerable<Order>> Orders(string id);
    public Task UpdateOrders(ProductIdDto idDto, OrderIdDto[] OrdersId);
    public Task UpdateProduct(string id, Product dto);
}

using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface ICustomersService
{
    public Task<Customer> CreateCustomer(CustomerCreateInput input);
    public Task ConnectOrder(string id, [Required] string OrderId);
    public Task ConnectAddress(string id, [Required] string AddressId);
    public Task DisconnectOrder(string id, [Required] string OrderId);
    public Task DisconnectAddress(string id, [Required] string AddressId);
    public Task<IEnumerable<Order>> Orders(string id);
    public Task<IEnumerable<Address>> Addresses(string id);
    public Task<IEnumerable<Order>> Orders(string id);
    public Task<IEnumerable<Address>> Addresses(string id);
    public Task UpdateOrders(CustomerIdDto idDto, OrderIdDto[] OrdersId);
    public Task UpdateAddresses(CustomerIdDto idDto, AddressIdDto[] AddressesId);
    public Task DeleteCustomer(string id);
    public Task<IEnumerable<Customer>> Customers();
    public Task UpdateCustomer(string id, Customer dto);
}

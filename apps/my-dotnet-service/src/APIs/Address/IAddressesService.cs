using System.ComponentModel.DataAnnotations;
using MyDotnetService.APIs.Dtos;

namespace MyDotnetService.APIs;

public interface IAddressesService
{
    public Task ConnectCustomer(string id, [Required] string CustomerId);
    public Task DisconnectCustomer(string id, [Required] string CustomerId);
    public Task<IEnumerable<Customer>> Customers(string id);
    public Task UpdateCustomers(AddressIdDto idDto, CustomerIdDto[] CustomersId);
    public Task<Address> CreateAddress(AddressCreateInput input);
    public Task DeleteAddress(string id);
    public Task<IEnumerable<Address>> Addresses();
    public Task UpdateAddress(string id, Address dto);
}

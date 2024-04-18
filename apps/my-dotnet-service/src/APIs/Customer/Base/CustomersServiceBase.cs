using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly MyDotnetServiceContext _context;

    public CustomersServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool CustomerExists(long id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }

    public async Task<CustomerDto> CreateCustomer(CustomerCreateInput inputDto)
    {
        var model = new Customer { Id = inputDto.Id, Name = inputDto.Name, };
        _context.customers.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Customer>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task ConnectOrder(string id, [Required] string orderId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var order = await _context.orders.FindAsync(orderId);
        if (order == null)
        {
            throw new NotFoundException();
        }

        customer.orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task ConnectAddress(string id, [Required] string addressId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var address = await _context.addresses.FindAsync(addressId);
        if (address == null)
        {
            throw new NotFoundException();
        }

        customer.addresses.Add(address);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectOrder(string id, [Required] string orderId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var order = await _context.orders.FindAsync(orderId);
        if (order == null)
        {
            throw new NotFoundException();
        }

        customer.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectAddress(string id, [Required] string addressId)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var address = await _context.addresses.FindAsync(addressId);
        if (address == null)
        {
            throw new NotFoundException();
        }

        customer.addresses.Remove(address);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderDto>> Orders(string id)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer.Orders.Select(order => order.ToDto()).ToList();
    }

    public async Task<IEnumerable<AddressDto>> Addresses(string id)
    {
        var customer = await _context.customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer.Addresses.Select(address => address.ToDto()).ToList();
    }

    public async Task UpdateOrders(CustomerIdDto idDto, OrderIdDto[] ordersId)
    {
        var customer = await _context
            .customers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var orders = await _context
            .Orders.Where(t => ordersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (orders.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Orders = orders;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAddresses(CustomerIdDto idDto, AddressIdDto[] addressesId)
    {
        var customer = await _context
            .customers.Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var addresses = await _context
            .Addresses.Where(t => addressesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (addresses.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Addresses = addresses;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(string id)
    {
        var customer = await _context.customers.FindAsync(id);

        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CustomerDto>> customers(CustomerFindMany findManyArgs)
    {
        var customers = await _context
            .customers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return customers.ConvertAll(customer => customer.ToDto());
    }

    public async Task UpdateCustomer(string id, CustomerDto customerDto)
    {
        var customer = new Customer { Id = customerDto.Id, Name = customerDto.Name, };

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}

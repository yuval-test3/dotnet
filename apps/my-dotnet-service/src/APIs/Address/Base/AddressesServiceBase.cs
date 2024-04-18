using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class AddressesServiceBase : IAddressesService
{
    protected readonly MyDotnetServiceContext _context;

    public AddressesServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool AddressExists(long id)
    {
        return _context.Addresses.Any(e => e.Id == id);
    }

    public async Task ConnectCustomer(string id, [Required] string customerId)
    {
        var address = await _context.addresses.FindAsync(id);
        if (address == null)
        {
            throw new NotFoundException();
        }

        var customer = await _context.customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        address.customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectCustomer(string id, [Required] string customerId)
    {
        var address = await _context.addresses.FindAsync(id);
        if (address == null)
        {
            throw new NotFoundException();
        }

        var customer = await _context.customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        address.customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CustomerDto>> Customers(string id)
    {
        var address = await _context.addresses.FindAsync(id);
        if (address == null)
        {
            throw new NotFoundException();
        }

        return address.Customers.Select(customer => customer.ToDto()).ToList();
    }

    public async Task UpdateCustomers(AddressIdDto idDto, CustomerIdDto[] customersId)
    {
        var address = await _context
            .addresses.Include(x => x.Customers)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (address == null)
        {
            throw new NotFoundException();
        }

        var customers = await _context
            .Customers.Where(t => customersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (customers.Count == 0)
        {
            throw new NotFoundException();
        }

        address.Customers = customers;
        await _context.SaveChangesAsync();
    }

    public async Task<AddressDto> CreateAddress(AddressCreateInput inputDto)
    {
        var model = new Address { Id = inputDto.Id, Name = inputDto.Name, };
        _context.addresses.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Address>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteAddress(string id)
    {
        var address = await _context.addresses.FindAsync(id);

        if (address == null)
        {
            throw new NotFoundException();
        }

        _context.addresses.Remove(address);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AddressDto>> addresses(AddressFindMany findManyArgs)
    {
        var addresses = await _context
            .addresses.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return addresses.ConvertAll(address => address.ToDto());
    }

    public async Task UpdateAddress(string id, AddressDto addressDto)
    {
        var address = new Address { Id = addressDto.Id, Name = addressDto.Name, };

        _context.Entry(address).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AddressExists(id))
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

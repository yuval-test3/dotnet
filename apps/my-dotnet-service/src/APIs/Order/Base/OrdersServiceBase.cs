using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly MyDotnetServiceContext _context;

    public OrdersServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool OrderExists(long id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }

    public async Task<OrderDto> CreateOrder(OrderCreateInput inputDto)
    {
        var model = new Order { Id = inputDto.Id, Name = inputDto.Name, };
        _context.orders.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Order>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteOrder(string id)
    {
        var order = await _context.orders.FindAsync(id);

        if (order == null)
        {
            throw new NotFoundException();
        }

        _context.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderDto>> orders(OrderFindMany findManyArgs)
    {
        var orders = await _context
            .orders.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return orders.ConvertAll(order => order.ToDto());
    }

    public async Task UpdateOrder(string id, OrderDto orderDto)
    {
        var order = new Order { Id = orderDto.Id, Name = orderDto.Name, };

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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

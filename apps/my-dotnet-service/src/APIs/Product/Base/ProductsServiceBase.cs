using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyDotnetService.APIs.Dtos;
using MyDotnetService.APIs.Errors;
using MyDotnetService.APIs.Extensions;
using MyDotnetService.Infrastructure;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly MyDotnetServiceContext _context;

    public ProductsServiceBase(MyDotnetServiceContext context)
    {
        _context = context;
    }

    private bool ProductExists(long id)
    {
        return _context.Products.Any(e => e.Id == id);
    }

    public async Task<ProductDto> CreateProduct(ProductCreateInput inputDto)
    {
        var model = new Product { Id = inputDto.Id, Name = inputDto.Name, };
        _context.products.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Product>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DeleteProduct(string id)
    {
        var product = await _context.products.FindAsync(id);

        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDto>> products(ProductFindMany findManyArgs)
    {
        var products = await _context
            .products.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return products.ConvertAll(product => product.ToDto());
    }

    public async Task ConnectOrder(string id, [Required] string orderId)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var order = await _context.orders.FindAsync(orderId);
        if (order == null)
        {
            throw new NotFoundException();
        }

        product.orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task DisconnectOrder(string id, [Required] string orderId)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var order = await _context.orders.FindAsync(orderId);
        if (order == null)
        {
            throw new NotFoundException();
        }

        product.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderDto>> Orders(string id)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product.Orders.Select(order => order.ToDto()).ToList();
    }

    public async Task UpdateOrders(ProductIdDto idDto, OrderIdDto[] ordersId)
    {
        var product = await _context
            .products.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (product == null)
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

        product.Orders = orders;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(string id, ProductDto productDto)
    {
        var product = new Product { Id = productDto.Id, Name = productDto.Name, };

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
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

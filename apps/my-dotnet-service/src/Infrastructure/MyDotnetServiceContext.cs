using Microsoft.EntityFrameworkCore;
using MyDotnetService.Infrastructure.Models;

namespace MyDotnetService.Infrastructure;

public class MyDotnetServiceContext : DbContext
{
    public MyDotnetServiceContext(DbContextOptions<MyDotnetServiceContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}

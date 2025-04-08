using Microsoft.EntityFrameworkCore;  // Required for DbContext, DbSet


// Data/OrderDbContext.cs
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}

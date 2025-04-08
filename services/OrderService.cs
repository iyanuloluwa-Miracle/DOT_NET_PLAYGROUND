using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _context;

    public OrderService(OrderDbContext context) => _context = context;

    public async Task<List<OrderResponse>> GetAllOrders()
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Select(o => new OrderResponse
            {
                OrderId = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Items = o.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToListAsync();
    }

    public async Task<OrderResponse?> GetOrderById(int id)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return null;

        return new OrderResponse
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            Items = order.Items.Select(i => new OrderItemResponse
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }

    public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return await GetOrderById(order.Id) ?? throw new Exception("Order creation failed.");
    }


    public async Task<OrderResponse?> UpdateOrder(int id, UpdateOrderRequest request)
    {
        var existingOrder = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (existingOrder == null)
            return null;

        // Update order properties
        existingOrder.CustomerId = request.CustomerId;
        
        // Remove existing items
        _context.OrderItems.RemoveRange(existingOrder.Items);
        
        // Add new items
        existingOrder.Items = request.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice
        }).ToList();

        await _context.SaveChangesAsync();

        return await GetOrderById(id);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

}

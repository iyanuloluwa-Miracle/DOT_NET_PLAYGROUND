public interface IOrderService
{
    Task<List<OrderResponse>> GetAllOrders();
    Task<OrderResponse?> GetOrderById(int id);
    Task<OrderResponse> CreateOrder(CreateOrderRequest request);
    Task<OrderResponse?> UpdateOrder(int id, UpdateOrderRequest request);
    Task<bool> DeleteOrder(int id);
}

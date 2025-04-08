public class OrderResponse
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemResponse> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(i => i.Total);
}

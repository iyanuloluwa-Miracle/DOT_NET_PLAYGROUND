public class UpdateOrderRequest
{
    public int CustomerId { get; set; }
    public List<UpdateOrderItemRequest> Items { get; set; } = new();
}

public class UpdateOrderItemRequest
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
using System.ComponentModel.DataAnnotations;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
}

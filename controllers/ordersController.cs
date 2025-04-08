using Microsoft.AspNetCore.Mvc;  // Required for ControllerBase, ApiController, Route, Http methods
using System.Collections.Generic;
using System.Threading.Tasks;


// Controllers/OrdersController.cs
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetOrders() =>
        Ok(await _service.GetAllOrders());

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetOrder(int id)
    {
        var order = await _service.GetOrderById(id);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(CreateOrderRequest request)
    {
        var order = await _service.CreateOrder(request);
        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderResponse>> UpdateOrder(int id, UpdateOrderRequest request)
    {
        var order = await _service.UpdateOrder(id, request);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _service.DeleteOrder(id);
        return result ? NoContent() : NotFound();
    }   
}

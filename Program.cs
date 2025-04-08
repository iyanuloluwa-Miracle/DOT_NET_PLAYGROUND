using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;  // Required for Swagger

 
var builder = WebApplication.CreateBuilder(args);
 
// Register services
builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("OrdersDb")); // In-memory database for simplicity
builder.Services.AddScoped<IOrderService, OrderService>(); // Dependency injection for OrderService
builder.Services.AddControllers(); // Add controllers to the service collection
builder.Services.AddEndpointsApiExplorer(); // Add support for API endpoints
builder.Services.AddSwaggerGen(); // Add Swagger for API documentation
 
var app = builder.Build(); // Build the application pipeline
 
if (app.Environment.IsDevelopment())  
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes to the application pipeline
 
app.Run();
using JensenOnline.Api.Data;
using JensenOnline.Api.DTOs;
using JensenOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JensenOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

        if (product == null)
            return BadRequest(new { message = "Produkten finns inte." });

        var order = new Order
        {
            UserId = userId,
            ProductName = product.Name,
            Price = product.Price
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        return Ok(await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync());
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await _context.Orders
            .Include(o => o.User)
            .Select(o => new
            {
                o.Id,
                o.ProductName,
                o.Price,
                o.OrderDate,
                Username = o.User!.Username
            })
            .ToListAsync());
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicStore.API.Data;
using ComicStore.API.Models;

namespace ComicStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdersController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/orders - Lay tat ca don hang
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Comic)
            .ToListAsync();
        return Ok(orders);
    }

    // GET: api/orders/5 - Lay don hang theo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Comic)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound(new { message = "Khong tim thay don hang" });
        return Ok(order);
    }

    // POST: api/orders - Tao don hang moi
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        order.CreatedAt = DateTime.UtcNow;
        order.Status = "Cho xu ly";
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    // PUT: api/orders/5 - Cap nhat don hang
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
        if (id != order.Id) return BadRequest(new { message = "ID khong khop" });
        _db.Entry(order).State = EntityState.Modified;
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Orders.Any(o => o.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // DELETE: api/orders/5 - Xoa don hang
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return NotFound(new { message = "Khong tim thay don hang" });
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

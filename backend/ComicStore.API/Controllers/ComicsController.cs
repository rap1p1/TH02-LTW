using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicStore.API.Data;
using ComicStore.API.Models;

namespace ComicStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComicsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ComicsController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/comics - Lay tat ca truyen
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comics = await _db.Comics.ToListAsync();
        return Ok(comics);
    }

    // GET: api/comics/5 - Lay truyen theo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comic = await _db.Comics.FindAsync(id);
        if (comic == null) return NotFound(new { message = "Khong tim thay truyen" });
        return Ok(comic);
    }

    // POST: api/comics - Them truyen moi
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Comic comic)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        comic.CreatedAt = DateTime.UtcNow;
        _db.Comics.Add(comic);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = comic.Id }, comic);
    }

    // PUT: api/comics/5 - Cap nhat truyen
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Comic comic)
    {
        if (id != comic.Id) return BadRequest(new { message = "ID khong khop" });
        _db.Entry(comic).State = EntityState.Modified;
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Comics.Any(c => c.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // DELETE: api/comics/5 - Xoa truyen
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comic = await _db.Comics.FindAsync(id);
        if (comic == null) return NotFound(new { message = "Khong tim thay truyen" });
        _db.Comics.Remove(comic);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

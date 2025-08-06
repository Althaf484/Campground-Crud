using CampgroundCrud.Api.Data;
using CampgroundCrud.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampgroundCrud.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CampgroundsController : ControllerBase
{

    private readonly AppDbContext db;
    public CampgroundsController(AppDbContext context)
    {
        db = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campground>>> GetAllCampgrounds()
    {
        var campgrounds = await db.Campgrounds
        .Include(c => c.Reviews)
        .AsNoTracking()
        .ToListAsync();

        if (!campgrounds.Any())
        {
            return NoContent();
        }

        return Ok(campgrounds);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Campground>> GetCampground(int id)
    {
        var campground = await db.Campgrounds
        .Include(c => c.Reviews)
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id);


        if (campground == null)
        {
            return NotFound();
        }

        return Ok(campground);
    }

    [HttpPost]
    public async Task<ActionResult<Campground>> CreateCampground([FromBody] Campground campground)
    {
        if (campground == null)
        {
            return BadRequest();
        }
        db.Campgrounds.Add(campground);

        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCampground), new { id = campground.Id }, campground);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCampground(int id, [FromBody] Campground updatedCampground)
    {
        if (id != updatedCampground.Id)
        {
            return BadRequest("ID mismatch between route and body");
        }

        var campground = await db.Campgrounds.FindAsync(id);

        if (campground == null)
        {
            return NotFound();
        }

        campground.Name = updatedCampground.Name;
        campground.Description = updatedCampground.Description;
        campground.ImageUrl = updatedCampground.ImageUrl;

        await db.SaveChangesAsync();

        return NoContent();
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampground(int id)
    {
        var campground = await db.Campgrounds.FindAsync(id);

        if (campground == null)
        {
            return NotFound();
        }

        db.Campgrounds.Remove(campground);
        await db.SaveChangesAsync();

        return NoContent();
    }


}

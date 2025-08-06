using Microsoft.AspNetCore.Mvc;
using CampgroundCrud.Api.Data;
using CampgroundCrud.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CampgroundCrud.Api.Controllers
{
    [ApiController]
    [Route("api/campgrounds/{campgroundId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext db;

        public ReviewsController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int campgroundId)
        {
            var campground = await db.Campgrounds
                .Include(c => c.Reviews)
                .FirstOrDefaultAsync(c => c.Id == campgroundId);

            if (campground == null)
            {
                return NotFound();
            }

            return Ok(campground.Reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(int campgroundId, [FromBody] Review review)
        {
            var campgroundExists = await db.Campgrounds.AnyAsync(c => c.Id == campgroundId);
            if (!campgroundExists)
            {
                return NotFound();
            }

            review.CampgroundId = campgroundId;

            db.Reviews.Add(review);
            await db.SaveChangesAsync();

            return Created($"api/campgrounds/{campgroundId}/reviews/{review.Id}", review);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int campgroundId, int reviewId, [FromBody] Review updatedReview)
        {
            if (reviewId != updatedReview.Id || campgroundId != updatedReview.CampgroundId)
            {
                return BadRequest();
            }

            var review = await db.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId && r.CampgroundId == campgroundId);

            if (review == null)
            {
                return NotFound();
            }

            review.Rating = updatedReview.Rating;
            review.Comment = updatedReview.Comment;

            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int campgroundId, int reviewId)
        {
            var review = await db.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId && r.CampgroundId == campgroundId);

            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}

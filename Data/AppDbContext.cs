using CampgroundCrud.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CampgroundCrud.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Campground> Campgrounds { get; set; }
    public DbSet<Review> Reviews { get; set; }
}

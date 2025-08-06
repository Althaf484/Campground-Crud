using CampgroundCrud.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CampgroundCrud.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campground>()
            .HasMany(c => c.Reviews)
            .WithOne()
            .HasForeignKey(r => r.CampgroundId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Campground> Campgrounds { get; set; }
    public DbSet<Review> Reviews { get; set; }
}

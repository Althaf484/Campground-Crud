namespace CampgroundCrud.Api.Models;

public class Campground
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

}

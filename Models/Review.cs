namespace CampgroundCrud.Api.Models;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = String.Empty;
    public int CampgroundId { get; set; } 

    public Campground Campground { get; set; } = null!;
}

namespace GesHomeLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public DateTime  ReleaseDate { get; set; } = DateTime.UtcNow;
    public IEnumerable<GenresList> Genres { get; set; } = new List<GenresList>();
    public required StatusesList Status { get; set; }
    public string? GivenTo  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
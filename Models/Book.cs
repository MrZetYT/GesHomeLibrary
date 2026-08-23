namespace GesHomeLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public int ReleaseYear { get; set; }
    public IEnumerable<GenresList> Genres { get; set; } = new List<GenresList>();
    public required StatusesList Status { get; set; }
    public string? GivenTo  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
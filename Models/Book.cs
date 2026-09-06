namespace GesHomeLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public int ReleaseYear { get; set; }
    public List<GenresList> Genres { get; set; } = [];
    public required StatusesList Status { get; set; }
    public string? GivenTo  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
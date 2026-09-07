using System.Collections.Immutable;
using GesHomeLibrary.Exceptions;

namespace GesHomeLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public int ReleaseYear { get; set; }
    public IReadOnlyList<GenresList> Genres => _genres.ToImmutableList();
    public required StatusesList Status { get; set; }
    public string? GivenTo  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    private List<GenresList> _genres =[];
    
    public void AddGenre(GenresList genre)
    {
        if (_genres.Contains(genre)) throw new DuplicateCollectionVariable("That genre is already exist");
        _genres.Add(genre);
    }

    public void RemoveGenre(GenresList genre)
    {
        if (_genres.Count==1)
        {
            throw new PossibleEmptyCollection("Nothing in genres after deleting");
        }
        _genres.Remove(genre);
    }

    public void SwapGenres(GenresList oldGenre, GenresList newGenre)
    {
        if (_genres.Contains(oldGenre) && _genres.Count==1)
        {
            _genres.Remove(oldGenre);
            _genres.Add(newGenre);
        }
        else throw new InvalidOperationException("Cannot swap genres");
    }
}
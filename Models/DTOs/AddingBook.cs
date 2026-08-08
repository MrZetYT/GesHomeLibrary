namespace GesHomeLibrary.Models.DTOs;

public record AddingBook(
    string Name,
    string Author,
    DateTime ReleaseDate,
    List<GenresList> Genres,
    StatusesList Status,
    string? GivenTo);
namespace GesHomeLibrary.Models.DTOs;

public record AddingBook(
    string Name,
    string Author,
    int ReleaseYear,
    List<GenresList> Genres,
    StatusesList Status,
    string? GivenTo);
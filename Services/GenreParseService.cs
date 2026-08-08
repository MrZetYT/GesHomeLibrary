using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class GenreParseService
{
    public GenresList GenreParseByName(string genreName)
    {
        return genreName switch
        {
            "Fiction" => GenresList.Fiction,
            "Adventure" => GenresList.Adventure,
            "Science Fiction" => GenresList.ScienceFiction,
            "Philosophy" => GenresList.Philosophy,
            "Psychology" =>GenresList.Psychology,
            "Programming" => GenresList.Programming,
            "Romance" => GenresList.Romance,
            "Fantasy" =>GenresList.Fantasy,
            "Detective Story" => GenresList.DetectiveStory,
            _ => throw new ArgumentException($"Genre {genreName} not found")
        };
    }
}
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class FilterBookService: IFilterBookService
{
    private readonly GenreParseService _genreParseService;
    private readonly StatusParseService _statusParseService;

    public FilterBookService(
        GenreParseService genreParseService,
        StatusParseService statusParseService)
    {
        _genreParseService = genreParseService;
        _statusParseService = statusParseService;
    }
    
    public IEnumerable<Book> FilterBooksByGenre(IEnumerable<Book> books, string genre)
    {
        return books.Where(x => x.Genres.Contains(_genreParseService.GenreParseByName(genre))).ToList();
    }

    public IEnumerable<Book> FilterBooksByAuthor(IEnumerable<Book> books, string author)
    {
        return books.Where(x=> x.Author.Contains(author)).ToList();
    }

    public IEnumerable<Book> FilterBooksByStatus(IEnumerable<Book> books, string status)
    {
        return books.Where(x=> x.Status.Equals(_statusParseService.StatusParseByName(status))).ToList();
    }
}
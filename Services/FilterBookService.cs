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
    
    public IEnumerable<Book> FilterBooksByGenre(IEnumerable<Book> books, GenresList genre)
    {
        return books.Where(x => x.Genres.Contains(genre)).ToList();
    }

    public IEnumerable<Book> FilterBooksByAuthor(IEnumerable<Book> books, string author)
    {
        return books.Where(x=> x.Author.Contains(author)).ToList();
    }

    public IEnumerable<Book> FilterBooksByStatus(IEnumerable<Book> books, StatusesList status)
    {
        return books.Where(x=> x.Status.Equals(status)).ToList();
    }
}
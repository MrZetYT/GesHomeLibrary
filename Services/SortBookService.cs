using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class SortBookService: ISortBookService
{
    public IEnumerable<Book> SortBooksByReleaseDate(IEnumerable<Book> books)
    {
        return books.OrderBy(b => b.ReleaseYear);
    }

    public IEnumerable<Book> SortBooksByName(IEnumerable<Book> books)
    {
        return books.OrderBy(b => b.Name);
    }

    public IEnumerable<Book> SortBooksByAuthor(IEnumerable<Book> books)
    {
        return books.OrderBy(b => b.Author);
    }
}
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Interfaces;

public interface ISortBookService
{
    IEnumerable<Book> SortBooksByReleaseDate(IEnumerable<Book> books);
    IEnumerable<Book> SortBooksByName(IEnumerable<Book> books);
    IEnumerable<Book> SortBooksByAuthor(IEnumerable<Book> books);
}
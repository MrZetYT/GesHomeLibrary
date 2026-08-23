using GesHomeLibrary.Models;

namespace GesHomeLibrary.Interfaces;

public interface IFilterBookService
{
    IEnumerable<Book> FilterBooksByGenre(IEnumerable<Book> books, string genre);
    IEnumerable<Book> FilterBooksByAuthor(IEnumerable<Book> books, string author);
    IEnumerable<Book> FilterBooksByStatus(IEnumerable<Book> books, string status);
}
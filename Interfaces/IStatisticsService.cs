using GesHomeLibrary.Models;

namespace GesHomeLibrary.Interfaces;

public interface IStatisticsService
{
    int CountBooks(IEnumerable<Book> books);
    int CountReadedBooks(IEnumerable<Book> books);
    int CountGivenAwayBooks(IEnumerable<Book> books);
    Dictionary<string, int> CountBooksByGenres(IEnumerable<Book> books);
}
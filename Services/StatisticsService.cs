using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class StatisticsService: IStatisticsService
{
    public int CountBooks(IEnumerable<Book> books)
    {
        return books.Distinct().Count();
    }

    public int CountReadedBooks(IEnumerable<Book> books)
    {
        return books.Count(x => x.Status == StatusesList.BeingRead);
    }

    public int CountGivenAwayBooks(IEnumerable<Book> books)
    {
        return books.Count(x => x.Status == StatusesList.GivenAway);
    }

    public Dictionary<string, int> CountBooksByGenres(IEnumerable<Book> books)
    {
        var result = new Dictionary<string, int>();
        foreach (var book in books)
        {
            var genres = book.Genres.ToList();
            foreach (var genre in genres)
            {
                if (result.ContainsKey(genre.ToString()))
                {
                    result[genre.ToString()]++;
                }
                else
                {
                    result[genre.ToString()] = 1;
                }
                
            }
        }

        return result;
    }
}
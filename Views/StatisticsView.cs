using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Views;

public class StatisticsView
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsView(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    public void ShowStatistics(IEnumerable<Book> books)
    {
        Console.WriteLine("Статистика Вашей библиотеки:");
        Console.WriteLine(new string('~',50));
        Console.WriteLine($"\nКоличество книг в библиотеке: {_statisticsService.CountBooks(books)}\n");
        Console.WriteLine(new string('~',50));
        Console.WriteLine($"\nКоличество прочитанных книг в библиотеке: {_statisticsService.CountReadedBooks(books)}\n");
        Console.WriteLine(new string('~',50));
        Console.WriteLine($"\nКоличество отданных книг в библиотеке: {_statisticsService.CountGivenAwayBooks(books)}\n");
        Console.WriteLine(new string('~',50));
        Console.WriteLine($"\nКоличество книг по жанрам в библиотеке:");
        
        var booksByGenres = _statisticsService.CountBooksByGenres(books);

        foreach (var book in booksByGenres)
        {
            Console.WriteLine($"{book.Key}: {book.Value}");
        }

        Console.WriteLine();
        Console.WriteLine(new string('~',50));
    }
}
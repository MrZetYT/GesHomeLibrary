using System.Text;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Views;

public class BaseView
{
    protected void ShowAllBooks(IEnumerable<Book> books)
    {
        foreach (var book in books)
        {
            var genresList = book.Genres;

            var sb = new StringBuilder();
            foreach (var genre in genresList)
            {
                sb.Append(genre.ToString()+", ");
            }

            sb.Remove(sb.Length - 2, 2);
            var givenTo = book.Status == StatusesList.GivenAway ? book.GivenTo : "";

            Console.WriteLine(new string('~',50));
            Console.WriteLine($"ID: {book.Id}\n" +
                              $"Название: {book.Name}\n" +
                              $"Автор: {book.Author}\n" +
                              $"Жанры: {sb}\n" +
                              $"Дата выхода: {book.ReleaseYear}\n" +
                              $"Статус: {book.Status.ToString()} {(book.Status==StatusesList.GivenAway ? $"- {givenTo}" : "")}\n" +
                              $"Создана: {book.CreatedAt}");
        }
        Console.WriteLine(new string('~',50));
    }

    protected void ShowAllBookGenres()
    {
        Console.WriteLine("Возможные жанры:\n" +
                          "1. Science Fiction\n" +
                          "2. Fantasy\n" +
                          "3. Adventure\n" +
                          "4. Romance\n" +
                          "5. Detective Story\n" +
                          "6. Psychology\n" +
                          "7. Philosophy\n" +
                          "8. Programming\n" +
                          "9. Fiction");
    }
}
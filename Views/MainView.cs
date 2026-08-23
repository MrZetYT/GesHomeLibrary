using System.Text;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Views;

public class MainView
{
    private readonly IBookService _bookService;
    private readonly BookCRUDView _bookCRUDView;
    
    public MainView(IBookService bookService,
            BookCRUDView bookCRUDView)
    {
        _bookService = bookService;
        _bookCRUDView = bookCRUDView;
    }
    
    public void StartMainView()
    {
        int choice = 0;
        while (choice != 6)
        {
            Console.WriteLine("Что бы вы хотели сделать?");
            Console.Write("1. Показать все книги\n" + 
                              "2. Операции по книге\n" +
                              "3. Фильтр по книгам\n" +
                              "4. Сортировка книг\n" +
                              "5. Статистика по книгам\n" +
                              "6. Выход\n" +
                              "Ввод: ");
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Неправильный ввод! Попробуйте еще раз!");
                Console.ReadKey();
                continue;
            }
            switch (choice)
            {
                case 1:
                {
                    ShowAllBooks(_bookService.GetBooks());
                    break;
                }
                case 2:
                {
                    _bookCRUDView.StartBookCRUDView();
                    break;
                }
                default:
                {
                    Console.WriteLine("Неправильный ввод! Попробуйте еще раз!");
                    Console.ReadKey();
                    continue;
                }
            }
        }
    }

    public static void ShowAllBooks(IEnumerable<Book> books)
    {
        foreach (var book in books)
        {
            var genresList = book.Genres.ToList();

            var sb = new StringBuilder();
            foreach (var genre in genresList)
            {
                sb.Append(genre.ToString()+" ");
            }
            var givenTo = book.Status == StatusesList.GivenAway ? book.GivenTo : "";

            Console.WriteLine(new string('~',50));
            Console.WriteLine($"ID: {book.Id}\n" +
                              $"Название: {book.Name}\n" +
                              $"Автор: {book.Author}\n" +
                              $"Жанры: {sb}\n" +
                              $"Дата выхода: {book.ReleaseYear}\n" +
                              $"Статус: {book.Status.ToString()} {givenTo}\n" +
                              $"Создана: {book.CreatedAt}");
        }
        Console.WriteLine(new string('~',50));
    }
}
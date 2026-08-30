using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Views;

public class SortView
{
    private readonly ISortBookService _sortBookService;
    private readonly IBookService _bookService;

    public SortView(ISortBookService sortBookService,
        IBookService bookService)
    {
        _sortBookService = sortBookService;
        _bookService = bookService;
    }

    public void StartSortView()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.Write("Доступный выбор сортировки:\n" +
                          "1. Год выхода\n" +
                          "2. Имя\n" +
                          "3. Автор\n" +
                          "4. Выход\n" +
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
            
            IEnumerable<Book> sortedBooks = null;

            switch (choice)
            {
                case 1:
                {
                    sortedBooks = _sortBookService.SortBooksByReleaseDate(_bookService.GetBooks());
                    
                    _bookService.ShowAllBooks(sortedBooks);

                    break;
                }
                case 2:
                {
                    sortedBooks = _sortBookService.SortBooksByName(_bookService.GetBooks());
                    
                    _bookService.ShowAllBooks(sortedBooks);

                    break;
                }
                case 3:
                {
                    sortedBooks = _sortBookService.SortBooksByAuthor(_bookService.GetBooks());
                    
                    _bookService.ShowAllBooks(sortedBooks);

                    break;
                }
            }
        }
    }
}
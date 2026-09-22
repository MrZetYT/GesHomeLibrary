using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class DeletingBookView : BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator _userInputValidator;
    private readonly BookValidator _bookValidator;

    public DeletingBookView(IBookService bookService,
        UserInputValidator userInputValidator,
        BookValidator bookValidator)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
        _bookValidator = bookValidator;
    }

    public void StartDeletingBookView()
    {
        var books = _bookService.GetBooks().ToList();
        if (!books.Any())
        {
            Console.WriteLine("Книги еще не добавлены! Отказано в доступе!");
            return;
        }

        Console.WriteLine("Что желаете удалить?\n" +
                          "1. Одну книгу\n" +
                          "2. ВСЕ книги\n" +
                          "3. Выход");

        int deleteChoice = _userInputValidator.NumberInput(1, 3);

        switch (deleteChoice)
        {
            case 1:
            {
                Console.WriteLine("Список существующих книг:");
                ShowAllBooks(books);

                Console.WriteLine("Введите ID книги для удаления");
                int bookIdChoice = _userInputValidator.NumberInput(0, books.Last().Id);
                if (!_bookValidator.IsBookExist(bookIdChoice, books))
                {
                    Console.WriteLine($"Книги с ID {bookIdChoice} не существует!");
                    break;
                }

                _bookService.DeleteBook(bookIdChoice);

                Console.WriteLine("Удаление прошло успешно!");

                break;
            }
            case 2:
            {
                Console.WriteLine("Уверены ли вы? (Y/N)");
                string agreeChoice = "";
                while (agreeChoice != "Y" && agreeChoice != "N")
                {
                    agreeChoice = _userInputValidator.StringInput();
                    Console.WriteLine("Неправильный ввод ответа! Попробуйте еще раз...");
                }

                if (agreeChoice == "Y")
                {
                    _bookService.DeleteAllBooks();
                    Console.WriteLine("Удаление произошло успешно!");
                }

                break;
            }
            case 3:
            {
                Console.WriteLine("Отмена удаления!");
                break;
            }
        }
    }
}
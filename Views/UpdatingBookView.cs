using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class UpdatingBookView : BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator _userInputValidator;
    private readonly BookValidator _bookValidator;
    private readonly UpdatingGenreView _updatingGenreView;

    public UpdatingBookView(IBookService bookService,
        UserInputValidator userInputValidator,
        BookValidator bookValidator,
        UpdatingGenreView updatingGenreView)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
        _bookValidator = bookValidator;
        _updatingGenreView = updatingGenreView;
    }

    public void StartUpdatingBookView()
    {
        var books = _bookService.GetBooks().ToList();
        if (!books.Any())
        {
            Console.WriteLine("Книги еще не добавлены! Отказано в доступе!");
            return;
        }

        Console.WriteLine("Список существующих книг:");
        ShowAllBooks(books);

        Console.WriteLine("Введите ID книги для изменения");
        int bookIdChoice = _userInputValidator.NumberInput(0, books.Last().Id);
        if (!_bookValidator.IsBookExist(bookIdChoice, books))
        {
            Console.WriteLine($"Книги с ID {bookIdChoice} не существует!");
            return;
        }

        Console.WriteLine("Доступные изменения: \n" +
                          "1. Название\n" +
                          "2. Автор\n" +
                          "3. Жанр\n" +
                          "4. Дата\n" +
                          "5. Статус\n" +
                          "6. Выход");

        Console.WriteLine("Введите номер изменения");
        int changeChoice = _userInputValidator.NumberInput(1, 6);

        switch (changeChoice)
        {
            case 1:
            {
                Console.WriteLine("Введите новое название");
                string newName = _userInputValidator.StringInput();

                _bookService.UpdateBookName(bookIdChoice, newName);

                Console.WriteLine("Обновлено успешно!");

                break;
            }
            case 2:
            {
                Console.WriteLine("Введите нового автора");
                string newAuthor = _userInputValidator.StringInput();

                _bookService.UpdateBookAuthor(bookIdChoice, newAuthor);

                Console.WriteLine("Обновлено успешно!");

                break;
            }
            case 3:
            {
                _updatingGenreView.StartUpdatingGenreView(bookIdChoice);
                break;
            }
            case 4:
            {
                Console.WriteLine("Введите новый год издания");
                int newYear = _userInputValidator.NumberInput(1, DateTime.Now.Year);

                _bookService.UpdateBookDate(bookIdChoice, newYear);

                Console.WriteLine("Обновлено успешно!");

                break;
            }
            case 5:
            {
                Console.WriteLine("Доступные статусы: \n" +
                                  "1. In Stock\n" +
                                  "2. Read\n" +
                                  "3. Given Away\n" +
                                  "4. Being Read");

                int newStatus = _userInputValidator.NumberInput(1, 4);

                string? givenTo = null;
                if (newStatus - 1 == (int)StatusesList.GivenAway)
                {
                    Console.WriteLine("Введите того, кому вы отдаете книгу");
                    givenTo = _userInputValidator.StringInput();
                }

                try
                {
                    _bookService.UpdateBookStatus(bookIdChoice, (StatusesList)newStatus - 1, givenTo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Обновление не удалось... {ex.Message}");
                    break;
                }

                Console.WriteLine("Обновлено успешно!");

                break;
            }
            case 6: break;
        }
    }
}
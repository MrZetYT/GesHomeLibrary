using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class FilterView : BaseView
{
    private readonly IFilterBookService _filterBookService;
    private readonly IBookService _bookService;
    private readonly UserInputValidator _userInputValidator;

    public FilterView(IFilterBookService filterBookService,
        IBookService bookService,
        UserInputValidator userInputValidator)
    {
        _filterBookService = filterBookService;
        _bookService = bookService;
        _userInputValidator = userInputValidator;
    }

    public void StartFilterView()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.WriteLine("Доступный выбор фильтров:\n" +
                          "1. Жанр\n" +
                          "2. Автор\n" +
                          "3. Статус\n" +
                          "4. Выход");

            choice = _userInputValidator.NumberInput(1, 4);

            switch (choice)
            {
                case 1:
                {
                    ShowAllBookGenres();
                    
                    Console.WriteLine("Введите номер жанра");
                    int genre = _userInputValidator.NumberInput(1,9);

                    var filteredBooks = _filterBookService
                        .FilterBooksByGenre(_bookService.GetBooks(), (GenresList)genre-1)
                        .ToList();

                    if (!filteredBooks.Any())
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    ShowAllBooks(filteredBooks);
                    
                    break;
                }
                case 2:
                {
                    Console.Write("Введите автора: ");
                    string author = _userInputValidator.StringInput();

                    var filteredBooks = _filterBookService
                        .FilterBooksByAuthor(_bookService.GetBooks(), author)
                        .ToList();

                    if (!filteredBooks.Any())
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    ShowAllBooks(filteredBooks);
                    
                    break;
                }
                case 3:
                {
                    ShowAllBookStatuses();
                    
                    int status = _userInputValidator.NumberInput(1,4);

                    var filteredBooks = _filterBookService
                        .FilterBooksByStatus(_bookService.GetBooks(), (StatusesList)status-1)
                        .ToList();

                    if (!filteredBooks.Any())
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    ShowAllBooks(filteredBooks);
                    
                    break;
                }
                case 4:
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
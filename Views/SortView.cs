using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class SortView : BaseView
{
    private readonly ISortBookService _sortBookService;
    private readonly IBookService _bookService;
    private readonly UserInputValidator _userInputValidator;

    public SortView(ISortBookService sortBookService,
        IBookService bookService,
        UserInputValidator userInputValidator)
    {
        _sortBookService = sortBookService;
        _bookService = bookService;
        _userInputValidator = userInputValidator;
    }

    public void StartSortView()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.WriteLine("Доступный выбор сортировки:\n" +
                          "1. Год выхода\n" +
                          "2. Имя\n" +
                          "3. Автор\n" +
                          "4. Выход");

            choice = _userInputValidator.NumberInput(1, 4);

            switch (choice)
            {
                case 1:
                {
                    ShowAllBooks(_sortBookService.SortBooksByReleaseDate(_bookService.GetBooks()));

                    break;
                }
                case 2:
                {
                    ShowAllBooks(_sortBookService.SortBooksByName(_bookService.GetBooks()));

                    break;
                }
                case 3:
                {
                    ShowAllBooks(_sortBookService.SortBooksByAuthor(_bookService.GetBooks()));

                    break;
                }
            }
        }
    }
}
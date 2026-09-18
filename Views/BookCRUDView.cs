using GesHomeLibrary.Exceptions;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class BookCrudView : BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator  _userInputValidator;
    private readonly BookValidator _bookValidator;

    private readonly AddingBookView _addingBookView;
    private readonly UpdatingBookView _updatingBookView;
    private readonly DeletingBookView _deletingBookView;

    public BookCrudView(IBookService bookService,
        UserInputValidator userInputValidator,
        BookValidator bookValidator,
        AddingBookView addingBookView,
        UpdatingBookView updatingBookView,
        DeletingBookView deletingBookView)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
        _bookValidator = bookValidator;
        _addingBookView = addingBookView;
        _updatingBookView = updatingBookView;
        _deletingBookView = deletingBookView;
    }
    public void StartBookCrudView()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.WriteLine("Доступные операции:\n" +
                          "1. Добавить книгу\n" +
                          "2. Обновление книг\n" +
                          "3. Удаление книг\n" +
                          "4. Выход\n");

            choice = _userInputValidator.NumberInput(1, 4);

            switch (choice)
            {
                case 1:
                {
                    _addingBookView.StartAddingBookView();
                    break;
                }

                case 2:
                {
                    _updatingBookView.StartUpdatingBookView();
                    break;
                }

                case 3:
                {
                    _deletingBookView.StartDeletingBookView();
                    break;
                }
            }
        }
        
    }
}
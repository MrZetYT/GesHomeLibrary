using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class BookCrudView : BaseView
{
    private readonly UserInputValidator  _userInputValidator;

    private readonly AddingBookView _addingBookView;
    private readonly UpdatingBookView _updatingBookView;
    private readonly DeletingBookView _deletingBookView;

    public BookCrudView(UserInputValidator userInputValidator,
        AddingBookView addingBookView,
        UpdatingBookView updatingBookView,
        DeletingBookView deletingBookView)
    {
        _userInputValidator = userInputValidator;
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
                    Console.Clear();
                    _addingBookView.StartAddingBookView();
                    break;
                }

                case 2:
                {
                    Console.Clear();
                    _updatingBookView.StartUpdatingBookView();
                    break;
                }

                case 3:
                {
                    Console.Clear();
                    _deletingBookView.StartDeletingBookView();
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
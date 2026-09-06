using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class MainView : BaseView
{
    private readonly IBookService _bookService;
    private readonly BookCrudView _bookCrudView;
    private readonly FilterView _filterView;
    private readonly SortView _sortView;
    private readonly StatisticsView _statisticsView;
    private readonly UserInputValidator _userInputValidator;
    
    public MainView(IBookService bookService,
            BookCrudView bookCrudView,
            FilterView filterView,
            SortView sortView,
            StatisticsView statisticsView,
            UserInputValidator  userInputValidator)
    {
        _bookService = bookService;
        _bookCrudView = bookCrudView;
        _filterView = filterView;
        _sortView = sortView;
        _statisticsView = statisticsView;
        _userInputValidator = userInputValidator;
    }
    
    public void StartMainView()
    {
        int choice = 0;
        while (choice != 6)
        {
            Console.WriteLine("Что бы вы хотели сделать?");
            Console.WriteLine("1. Показать все книги\n" + 
                          "2. Операции по книге\n" +
                          "3. Фильтр по книгам\n" + 
                          "4. Сортировка книг\n" + 
                          "5. Статистика по книгам\n" + 
                          "6. Выход");

            choice = _userInputValidator.NumberInput(1, 6);
            
            switch (choice)
            {
                case 1:
                {
                    ShowAllBooks(_bookService.GetBooks());
                    break;
                }
                case 2:
                {
                    _bookCrudView.StartBookCrudView();
                    break;
                }
                case 3:
                {
                    _filterView.StartFilterView();
                    break;
                }
                case 4:
                {
                    _sortView.StartSortView();
                    break;
                }
                case 5:
                {
                    _statisticsView.ShowStatistics(_bookService.GetBooks());
                    break;
                }
                case 6:
                {
                    Console.WriteLine("Всего хорошего!!!");
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
}
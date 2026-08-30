using GesHomeLibrary.Interfaces;

namespace GesHomeLibrary.Views;

public class MainView
{
    private readonly IBookService _bookService;
    private readonly BookCRUDView _bookCRUDView;
    private readonly FilterView _filterView;
    private readonly SortView _sortView;
    
    public MainView(IBookService bookService,
            BookCRUDView bookCRUDView,
            FilterView filterView,
            SortView sortView)
    {
        _bookService = bookService;
        _bookCRUDView = bookCRUDView;
        _filterView = filterView;
        _sortView = sortView;
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
                    _bookService.ShowAllBooks(_bookService.GetBooks());
                    break;
                }
                case 2:
                {
                    _bookCRUDView.StartBookCRUDView();
                    break;
                }
                case 3:
                {
                    _filterView.StartFilteriew();
                    break;
                }
                case 4:
                {
                    _sortView.StartSortView();
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
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;

namespace GesHomeLibrary.Views;

public class FilterView
{
    private readonly IFilterBookService _filterBookService;
    private readonly IBookService _bookService;

    public FilterView(IFilterBookService filterBookService,
        IBookService bookService)
    {
        _filterBookService = filterBookService;
        _bookService = bookService;
    }

    public void StartFilteriew()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.Write("Доступный выбор фильтров:\n" +
                          "1. Жанр\n" +
                          "2. Автор\n" +
                          "3. Статус\n" +
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

            switch (choice)
            {
                case 1:
                {
                    Console.WriteLine("Возможные жанры:\n" +
                                      "1. Science Fiction\n" +
                                      "2. Fantasy\n" +
                                      "3. Adventure\n" +
                                      "4. Romance\n" +
                                      "5. Detective Story\n" +
                                      "6. Psychology\n" +
                                      "7. Philosophy\n" +
                                      "8. Programming\n" +
                                      "9. Fiction");
                    int genre = 0;
                    while (genre <= 0 || genre > 9)
                    {
                            
                        Console.Write("Введите номер жанра: ");
                        try
                        {
                            genre = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод жанра! Попробуйте еще раз...");
                        }
                    }

                    var filteredBooks = _filterBookService.FilterBooksByGenre(_bookService.GetBooks(), (GenresList)genre-1);

                    if (filteredBooks.Count() == 0)
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    _bookService.ShowAllBooks(filteredBooks);
                    
                    break;
                }
                case 2:
                {
                    string author = "";
                    while (String.IsNullOrWhiteSpace(author))
                    {
                        Console.Write("Введите автора: ");
                        try
                        {
                            author = Console.ReadLine();
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод автора! Попробуйте еще раз...");
                        }
                    }

                    var filteredBooks = _filterBookService.FilterBooksByAuthor(_bookService.GetBooks(), author);

                    if (filteredBooks.Count() == 0)
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    _bookService.ShowAllBooks(filteredBooks);
                    
                    break;
                }
                case 3:
                {
                    Console.WriteLine("Доступные статусы: \n" +
                                      "1. In Stock\n" +
                                      "2. Read\n" +
                                      "3. Given Away\n" +
                                      "4. Being Read");
                    
                    int status = 0;
                    while (status <= 0 || status > 4)
                    {
                        Console.Write("Ввод: ");
                        try
                        {
                            status = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод статуса! Попробуйте еще раз...");
                        }
                    }

                    var filteredBooks = _filterBookService.FilterBooksByStatus(_bookService.GetBooks(), (StatusesList)status-1);

                    if (filteredBooks.Count() == 0)
                    {
                        Console.WriteLine("Ничего не найдено по данному фильтру!");
                        break;
                    }
                    
                    _bookService.ShowAllBooks(filteredBooks);
                    
                    break;
                }
            }
        }
    }
}
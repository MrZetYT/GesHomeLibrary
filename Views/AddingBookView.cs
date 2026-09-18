using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class AddingBookView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator  _userInputValidator;

    public AddingBookView(IBookService bookService,
        UserInputValidator userInputValidator)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
    }
    
    public void StartAddingBookView()
    {
        Console.WriteLine("Введите название книги");
                    var name = _userInputValidator.StringInput();

                    Console.WriteLine("Введите автора книги");
                    var author = _userInputValidator.StringInput();
                    
                    Console.WriteLine("Введите год издания");
                    var releaseYear = _userInputValidator.NumberInput(1, DateTime.Now.Year);
                    
                    Console.WriteLine("Введите количество жанров (до 3 штук)");
                    int genresCount = _userInputValidator.NumberInput(1,3);

                    var genres = new List<GenresList>();
                    for (int i = 0; i < genresCount; i++)
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
                        
                        Console.WriteLine("Введите номер жанра");
                        int genre = _userInputValidator.NumberInput(1,9);
                        
                        genres.Add((GenresList)genre-1);
                    }

                    Console.WriteLine("Доступные статусы: \n" +
                                      "1. In Stock\n" +
                                      "2. Read\n" +
                                      "3. Given Away\n" +
                                      "4. Being Read");
                    
                    int statusChoice = _userInputValidator.NumberInput(1,4);

                    StatusesList status = (StatusesList)statusChoice-1;

                    var givenTo = "";
                    if (status == StatusesList.GivenAway)
                    {
                        Console.WriteLine("Введите, кому была отдана книга");
                        givenTo = _userInputValidator.StringInput();
                    }
                    
                    _bookService.AddBook(new AddingBook(name, author, releaseYear, genres, status, givenTo));
    }
}
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class BookCrudView : BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator  _userInputValidator;

    public BookCrudView(IBookService bookService,
        UserInputValidator userInputValidator)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
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
                    break;
                }

                case 2:
                {
                    if (!_bookService.GetBooks().Any())
                    {
                        Console.WriteLine("Книги еще не добавлены! Отказано в доступе!");
                        break;
                    }
                    Console.WriteLine("Список существующих книг:");
                    ShowAllBooks(_bookService.GetBooks());
                    
                    Console.WriteLine("Введите ID книги для изменения");
                    int bookIdChoice = _userInputValidator.NumberInput(0,_bookService.GetBooks().Last().Id);

                    Console.WriteLine("Доступные изменения: \n" +
                                      "1. Название\n" +
                                      "2. Автор\n" +
                                      "3. Жанр\n" +
                                      "4. Дата\n" +
                                      "5. Статус\n" +
                                      "6. Выход");
                    
                    Console.WriteLine("Введите номер изменения");
                    int changeChoice = _userInputValidator.NumberInput(1,6);
                    
                    switch(changeChoice)
                    {
                        case 1:
                        {
                            Console.WriteLine("Введите новое название");
                            string newName = _userInputValidator.StringInput();
                            
                            _bookService.UpdateBookName(bookIdChoice,  newName);
                            
                            Console.WriteLine("Обновлено успешно!");

                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("Введите нового автора");
                            string newAuthor = _userInputValidator.StringInput();
                            
                            _bookService.UpdateBookAuthor(bookIdChoice,  newAuthor);
                            
                            Console.WriteLine("Обновлено успешно!");

                            break;
                        }
                        case 3:
                        {
                            Console.WriteLine("Что вы желаете сделать с жанрами: \n" +
                                              "1. Удалить\n" +
                                              "2. Добавить");
                            
                            int genreActionChoice = _userInputValidator.NumberInput(1,2);

                            switch (genreActionChoice)
                            {
                                case 1:
                                {
                                    Console.WriteLine("Список жанров выбранной книги:");
                                    var genres = _bookService.GetBook(bookIdChoice).Genres.ToArray();
                                    var genresCount = genres.Count();
                                    for (int i = 0; i < genresCount; i++)
                                    {
                                        Console.WriteLine($"{i+1}. {genres[i]}");
                                    }

                                    Console.WriteLine($"{genresCount+1}. Удалить все жанры");

                                    int genreToDeleteChoice = _userInputValidator.NumberInput(1, genresCount+1);

                                    if (genreToDeleteChoice == genresCount + 1)
                                    {
                                        Console.WriteLine("Книга должна иметь хотя бы один жанр. Запускаю добавление жанра...");
                                        var newBookGenre = GetNewBookGenre();
                                        _bookService.DeleteAllGenres(bookIdChoice, newBookGenre);
                                        break;
                                    }
                                    
                                    _bookService.DeleteGenre(bookIdChoice,genres[genreToDeleteChoice-1]);
                                    
                                    break;
                                }
                                case 2:
                                {
                                    var newBookGenre = GetNewBookGenre();
                                    _bookService.UpdateBookGenre(bookIdChoice, newBookGenre);
                                    break;
                                }
                            }
                            
                            Console.WriteLine("Обновлено успешно!");

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
                    
                            int newStatus = _userInputValidator.NumberInput(1,4);

                            string? givenTo = null;
                            if (newStatus - 1 == (int)StatusesList.GivenAway)
                            {
                                Console.WriteLine("Введите того, кому вы отдаете книгу");
                                givenTo = _userInputValidator.StringInput();
                            }
                            
                            _bookService.UpdateBookStatus(bookIdChoice, (StatusesList)newStatus-1, givenTo);
                            
                            Console.WriteLine("Обновлено успешно!");

                            break;
                        }
                        case 6: break;
                    }
                    break;
                }

                case 3:
                {
                    if (!_bookService.GetBooks().Any())
                    {
                        Console.WriteLine("Книги еще не добавлены! Отказано в доступе!");
                        break;
                    }
                    Console.WriteLine("Что желаете удалить?\n" +
                                      "1. Одну книгу\n" +
                                      "2. ВСЕ книги\n" +
                                      "3. Выход");

                    int deleteChoice = _userInputValidator.NumberInput(1,3);

                    switch (deleteChoice)
                    {
                        case 1:
                        {
                            Console.WriteLine("Список существующих книг:");
                            ShowAllBooks(_bookService.GetBooks());
                            
                            Console.WriteLine("Введите ID книги для удаления");
                            int bookIdChoice = _userInputValidator.NumberInput(0, _bookService.GetBooks().Last().Id);
                            
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
                    break;
                }
            }
        }
        
    }

    private GenresList GetNewBookGenre()
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
        
        Console.WriteLine("Введите номер нового жанра");
        int genre = _userInputValidator.NumberInput(1,9);
                            
        return (GenresList)genre-1;
    }
}
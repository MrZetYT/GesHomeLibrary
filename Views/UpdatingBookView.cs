using GesHomeLibrary.Exceptions;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class UpdatingBookView: BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator  _userInputValidator;
    private readonly BookValidator _bookValidator;

    public UpdatingBookView(IBookService bookService,
        UserInputValidator userInputValidator,
        BookValidator bookValidator)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
        _bookValidator = bookValidator;
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
                    int bookIdChoice = _userInputValidator.NumberInput(0,books.Last().Id);
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
                                        _bookService.DeleteAllGenres(bookIdChoice, GetNewBookGenre());
                                        break;
                                    }

                                    try
                                    {
                                        _bookService.DeleteGenre(bookIdChoice, genres[genreToDeleteChoice - 1]);
                                    }
                                    catch (PossibleEmptyCollection ex)
                                    {
                                        Console.WriteLine($"Произошла ошибка. {ex.Message}");
                                        Console.WriteLine("Книга должна иметь хотя бы один жанр. Запускаю добавление жанра...");
                                        _bookService.DeleteGenre(bookIdChoice, genres[genreToDeleteChoice-1], GetNewBookGenre());
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Что-то пошло не так. Удаление не выполнено. {ex.Message}");
                                    }
                                    
                                    break;
                                }
                                case 2:
                                {
                                    var newBookGenre = GetNewBookGenre();
                                    try
                                    {
                                        _bookService.UpdateBookGenre(bookIdChoice, newBookGenre);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine($"Что-то пошло не так. Обновление не выполнено\n" +
                                                          $"{ex.Message}");
                                        break;
                                    }
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

                            try
                            {
                                _bookService.UpdateBookStatus(bookIdChoice, (StatusesList)newStatus - 1, givenTo);
                            }
                            catch(Exception ex)
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
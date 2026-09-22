using GesHomeLibrary.Exceptions;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class UpdatingGenreView : BaseView
{
    private readonly IBookService _bookService;
    private readonly UserInputValidator _userInputValidator;

    public UpdatingGenreView(IBookService bookService,
        UserInputValidator userInputValidator)
    {
        _bookService = bookService;
        _userInputValidator = userInputValidator;
    }
    
    public void StartUpdatingGenreView(int bookIdChoice)
    {
        Console.WriteLine("Что вы желаете сделать с жанрами: \n" +
                          "1. Удалить\n" +
                          "2. Добавить");

        int genreActionChoice = _userInputValidator.NumberInput(1, 2);

        switch (genreActionChoice)
        {
            case 1:
            {
                Console.WriteLine("Список жанров выбранной книги:");
                var genres = _bookService.GetBook(bookIdChoice).Genres.ToArray();
                var genresCount = genres.Count();
                for (int i = 0; i < genresCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {genres[i]}");
                }

                Console.WriteLine($"{genresCount + 1}. Удалить все жанры");

                int genreToDeleteChoice = _userInputValidator.NumberInput(1, genresCount + 1);

                if (genreToDeleteChoice == genresCount + 1)
                {
                    Console.WriteLine("Книга должна иметь хотя бы один жанр. Запускаю добавление жанра...");
                    _bookService.DeleteAllGenres(bookIdChoice, GetNewBookGenre());
                    break;
                }

                if (genresCount == 1)
                {
                    Console.WriteLine("Книга должна иметь хотя бы один жанр. Запускаю добавление жанра...");
                    _bookService.DeleteGenre(bookIdChoice, genres[genreToDeleteChoice - 1], GetNewBookGenre());
                    break;
                }

                try
                {
                    _bookService.DeleteGenre(bookIdChoice, genres[genreToDeleteChoice - 1]);
                }
                catch (PossibleEmptyCollection ex)
                {
                    Console.WriteLine($"Произошла ошибка. Коллекция может быть пуста. {ex.Message}");
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Что-то пошло не так. Обновление не выполнено\n" +
                                      $"{ex.Message}");
                }

                break;
            }
        }

        Console.WriteLine("Обновлено успешно!");
    }
    
    private GenresList GetNewBookGenre()
    {
        ShowAllBookGenres();

        Console.WriteLine("Введите номер нового жанра");
        int genre = _userInputValidator.NumberInput(1, 9);

        return (GenresList)genre - 1;
    }
}
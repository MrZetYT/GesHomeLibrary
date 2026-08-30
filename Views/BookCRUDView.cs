using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;
using GesHomeLibrary.Services;

namespace GesHomeLibrary.Views;

public class BookCrudView
{
    private readonly IBookService _bookService;

    public BookCrudView(IBookService bookService)
    {
        _bookService = bookService;
    }
    public void StartBookCrudView()
    {
        int choice = 0;
        while (choice != 4)
        {
            Console.Write("Доступные операции:\n" +
                          "1. Добавить книгу\n" +
                          "2. Обновление книг\n" +
                          "3. Удаление книг\n" +
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
                    Console.Write("Введите название книги: ");
                    var name = Console.ReadLine();

                    Console.Write("Введите автора книги: ");
                    var author = Console.ReadLine();
                    
                    var releaseYear = int.MaxValue;
                    while (releaseYear <= 0 || releaseYear > DateTime.Now.Year)
                    {
                        Console.Write("Введите год издания: ");
                        try
                        {
                            releaseYear = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Год введен неверно! Попробуйте еще раз...");
                        }
                    }
                    
                    int genresCount = 0;
                    while (genresCount <= 0 || genresCount > 3)
                    {
                        Console.Write("Введите количество жанров (до 3 штук): ");
                        try
                        {
                            genresCount = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильно введено количество жанров! Попробуйте еще раз");
                        }
                    }

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

                        switch (genre)
                        {
                            case 1: genres.Add(GenresList.ScienceFiction); break;
                            case 2: genres.Add(GenresList.Fantasy); break;
                            case 3: genres.Add(GenresList.Adventure); break;
                            case 4: genres.Add(GenresList.Romance); break;
                            case 5: genres.Add(GenresList.DetectiveStory); break;
                            case 6: genres.Add(GenresList.Psychology); break;
                            case 7: genres.Add(GenresList.Philosophy); break;
                            case 8: genres.Add(GenresList.Programming); break;
                            case 9: genres.Add(GenresList.Fiction); break;
                        }
                    }

                    Console.WriteLine("Доступные статусы: \n" +
                                      "1. In Stock\n" +
                                      "2. Read\n" +
                                      "3. Given Away\n" +
                                      "4. Being Read");
                    
                    int statusChoice = 0;
                    while (statusChoice <= 0 || statusChoice > 4)
                    {
                        Console.Write("Ввод: ");
                        try
                        {
                            statusChoice = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод статуса! Попробуйте еще раз...");
                        }
                    }

                    StatusesList status = 0;
                    switch (statusChoice)
                    {
                        case 1: status=StatusesList.InStock; break;
                        case 2: status=StatusesList.Read; break;
                        case 3: status=StatusesList.GivenAway; break;
                        case 4: status=StatusesList.BeingRead; break;
                    }

                    var givenTo = "";
                    if (status == StatusesList.GivenAway)
                    {
                        Console.Write("Введите, кому была отдана книга: ");
                        givenTo = Console.ReadLine();
                    }
                    
                    _bookService.AddBook(new AddingBook(name, author, releaseYear, genres, status, givenTo));
                    break;
                }

                case 2:
                {
                    Console.WriteLine("Список существующих книг:");
                    _bookService.ShowAllBooks(_bookService.GetBooks());
                    int bookIdChoice = -1;
                    while (bookIdChoice < 0 || bookIdChoice > _bookService.GetBooks().Last().Id)
                    {
                        Console.Write("Введите ID книги для изменения: ");
                        try
                        {
                            bookIdChoice = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод ID! Попробуйте еще раз...");
                        }
                    }

                    Console.WriteLine("Доступные изменения: \n" +
                                      "1. Название\n" +
                                      "2. Автор\n" +
                                      "3. Жанр\n" +
                                      "4. Дата\n" +
                                      "5. Статус\n" +
                                      "6. Выход");
                    int changeChoice = 0;
                    while (changeChoice <= 0 || changeChoice > 6)
                    {
                        Console.Write("Введите номер изменения: ");
                        try
                        {
                            changeChoice = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод номер изменения! Попробуйте еще раз...");
                        }
                    }
                    
                    switch(changeChoice)
                    {
                        case 1:
                        {
                            Console.Write("Введите новое название: ");
                            string newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {
                                _bookService.UpdateBookName(bookIdChoice,  newName);
                            }
                            else
                            {
                                Console.WriteLine("Ошибка в вводе! Не удалось поменять название!");
                            }
                            Console.WriteLine("Обновлено успешно!");

                            break;
                        }
                        case 2:
                        {
                            Console.Write("Введите нового автора: ");
                            string newAuthor = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newAuthor))
                            {
                                _bookService.UpdateBookAuthor(bookIdChoice,  newAuthor);
                            }
                            else
                            {
                                Console.WriteLine("Ошибка в вводе! Не удалось поменять автора!");
                            }
                            Console.WriteLine("Обновлено успешно!");

                            break;
                        }
                        case 3:
                        {
                            Console.WriteLine("Что вы желаете сделать с жанрами: \n" +
                                              "1. Удалить\n" +
                                              "2. Добавить");
                            int genreActionChoice = 0;
                            while (genreActionChoice <= 0 || genreActionChoice > 2)
                            {
                                Console.Write("Ввод выбора: ");
                                try
                                {
                                    genreActionChoice = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("Неправильный выбор действия над жанрами! Попробуйте еще раз...");
                                }
                            }

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

                                    int genreToDeleteChoice = 0;
                                    while (genreToDeleteChoice <= 0 || genreToDeleteChoice > genresCount+1)
                                    {
                                        Console.Write("Ввод выбора: ");
                                        try
                                        {
                                            genreToDeleteChoice = int.Parse(Console.ReadLine());
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Неправильный выбор номера жанра! Попробуйте еще раз...");
                                        }
                                    }

                                    if (genreToDeleteChoice == genresCount + 1)
                                    {
                                        _bookService.DeleteAllGenres(bookIdChoice);
                                        Console.WriteLine("Книга должна иметь хотя бы один жанр. Запускаю добавление жанра...");
                                        var newBookGenre = GetNewBookGenre();
                                        _bookService.UpdateBookGenre(bookIdChoice, newBookGenre);
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
                            int newYear = 0;
                            while (newYear <= 0 || newYear > DateTime.Now.Year)
                            {
                                Console.Write("Введите новый год издания: ");
                                try
                                {
                                    newYear = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("Год введен неверно! Попробуйте еще раз...");
                                }
                            }
                            
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
                    
                            int newStatus = 0;
                            while (newStatus <= 0 || newStatus > 4)
                            {
                                Console.Write("Ввод: ");
                                try
                                {
                                    newStatus = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("Неправильный ввод статуса! Попробуйте еще раз...");
                                }
                            }

                            if (_bookService.GetBook(bookIdChoice).Status == (StatusesList)newStatus - 1)
                            {
                                Console.WriteLine("Невозможно одолжить уже одолженную книгу." +
                                                  "Ее необходимо вернуть!");
                                break;
                            }

                            string? givenTo = null;
                            if (newStatus - 1 == (int)StatusesList.GivenAway)
                            {
                                Console.Write("Введите того, кому вы отдаете книгу: ");
                                givenTo = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(givenTo))
                                {
                                    Console.WriteLine("Ошибка в вводе получателя! Не удалось поменять статус!");
                                    break;
                                }
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
                    Console.WriteLine("Что желаете удалить?\n" +
                                      "1. Одну книгу\n" +
                                      "2. ВСЕ книги\n" +
                                      "3. Выход");

                    int deleteChoice = 0;
                    while (deleteChoice <= 0 || deleteChoice > 3)
                    {
                        Console.Write("Введите номер: ");
                        try
                        {
                            deleteChoice = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Неправильный ввод номера! Попробуйте еще раз...");
                        }
                    }

                    switch (deleteChoice)
                    {
                        case 1:
                        {
                            Console.WriteLine("Список существующих книг:");
                            _bookService.ShowAllBooks(_bookService.GetBooks());
                            int bookIdChoice = -1;
                            while (bookIdChoice < 0 || bookIdChoice > _bookService.GetBooks().Last().Id)
                            {
                                Console.Write("Введите ID книги для удаления: ");
                                try
                                {
                                    bookIdChoice = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("Неправильный ввод ID! Попробуйте еще раз...");
                                }
                            }
                            
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
                                agreeChoice = Console.ReadLine();
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

    private static GenresList GetNewBookGenre()
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
                            
            Console.Write("Введите номер нового жанра: ");
            try
            {
                genre = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Неправильный ввод жанра! Попробуйте еще раз...");
            }
        }
                            
        return (GenresList)genre-1;
    }
}
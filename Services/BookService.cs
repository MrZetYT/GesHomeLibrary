using System.Text;
using GesHomeLibrary.Models;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models.DTOs;

namespace GesHomeLibrary.Services;

public class BookService: IBookService
{
    private readonly GenreParseService _genreParseService;
    private readonly StatusParseService _statusParseService;

    public BookService(
        GenreParseService genreParseService,
        StatusParseService statusParseService)
    {
        _genreParseService = genreParseService;
        _statusParseService = statusParseService;
    }

    public List<Book> Books { get; set; } = new();
    
    public void AddBook(AddingBook book)
    {
        int newId = Books.Count==0 ? 0 : ++Books.Last().Id;
        Books.Add(new Book
        {
            Id = newId,
            Name = book.Name,
            Author = book.Author,
            ReleaseYear = book.ReleaseYear,
            Genres = book.Genres,
            Status = book.Status,
            GivenTo = String.IsNullOrEmpty(book.GivenTo) ? null : book.GivenTo,
            CreatedAt = DateTime.UtcNow
        });
    }

    public void UpdateBookName(int bookId, string  name)
    {
        var book = GetBook(bookId);
        book.Name = name;
    }

    public void UpdateBookAuthor(int bookId, string author)
    {
        var book = GetBook(bookId);
        book.Author = author;
    }

    public void UpdateBookGenre(int bookId, GenresList genre)
    {
        var book = GetBook(bookId);
        var genres = book.Genres.ToList();
        genres.Add(genre);
        book.Genres = genres;
    }

    public void UpdateBookDate(int bookId, int year)
    {
        var book = GetBook(bookId);
        book.ReleaseYear = year;
    }

    public void UpdateBookStatus(int bookId, StatusesList status, string? givenTo)
    {
        var book = GetBook(bookId);
        
        book.Status = status;
        if (givenTo != null)
        {
            book.GivenTo = givenTo;
        }

        if (book.Status != StatusesList.GivenAway && book.GivenTo != null)
        {
            book.GivenTo = null;
        }
    }

    public IEnumerable<Book> GetBooks()
    {
        return Books;
    }
    
    public Book GetBook(int id)
    {
        var book = Books.FirstOrDefault(x => x.Id == id);
        if(book==null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found");
        }
        return book;
    }
    
    public void DeleteBook(int bookId)
    {
        var book = GetBook(bookId);
        Books.Remove(book);
    }

    public void DeleteAllBooks()
    {
        Books.Clear();
    }

    public void DeleteGenre(int bookId, GenresList genre)
    {
        var book = GetBook(bookId);
        var genres = book.Genres.ToList();
        genres.Remove(genre);
        book.Genres = genres;
    }

    public void DeleteAllGenres(int bookId)
    {
        var book = GetBook(bookId);
        book.Genres = new List<GenresList>();
    }
    
    public void ShowAllBooks(IEnumerable<Book> books)
    {
        foreach (var book in books)
        {
            var genresList = book.Genres.ToList();

            var sb = new StringBuilder();
            foreach (var genre in genresList)
            {
                sb.Append(genre.ToString()+", ");
            }

            sb.Remove(sb.Length - 2, 2);
            var givenTo = book.Status == StatusesList.GivenAway ? book.GivenTo : "";

            Console.WriteLine(new string('~',50));
            Console.WriteLine($"ID: {book.Id}\n" +
                              $"Название: {book.Name}\n" +
                              $"Автор: {book.Author}\n" +
                              $"Жанры: {sb}\n" +
                              $"Дата выхода: {book.ReleaseYear}\n" +
                              $"Статус: {book.Status.ToString()} {(book.Status==StatusesList.GivenAway ? $"- {givenTo}" : "")}\n" +
                              $"Создана: {book.CreatedAt}");
        }
        Console.WriteLine(new string('~',50));
    }
}
using GesHomeLibrary.Models;
using GesHomeLibrary.Interfaces;
using GesHomeLibrary.Models.DTOs;

namespace GesHomeLibrary.Services;

public class BookService: IBookService
{
    public List<Book> Books { get; set; } = new();
    
    public void AddBook(AddingBook book)
    {
        int newId = Books.Count==0 ? -1 : Books.Last().Id;
        Books.Add(new Book
        {
            Id = ++newId,
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
        var genres = book.Genres;
        if (genres.Contains(genre)) throw new InvalidOperationException("That genre is already exist");
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
        
        if (book.Status == status && status == StatusesList.GivenAway)
        {
            throw new InvalidOperationException("Impossible to borrow a borrowed book");
        }
        
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
        var genres = book.Genres;
        if (!genres.Any())
        {
            throw new InvalidOperationException("Nothing in genres");
        }
        genres.Remove(genre);
    }
    
    public void DeleteGenre(int bookId, GenresList genre, GenresList newGenre)
    {
        var book = GetBook(bookId);
        var genres = book.Genres;
        genres.Remove(genre);
        book.Genres = genres;
        if (!genres.Any())
        {
            UpdateBookGenre(bookId, newGenre);
        }
    }

    public void DeleteAllGenres(int bookId, GenresList newGenre)
    {
        var book = GetBook(bookId);
        book.Genres = new List<GenresList>();
        UpdateBookGenre(bookId, newGenre);
    }
}
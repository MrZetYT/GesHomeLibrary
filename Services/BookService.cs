using GesHomeLibrary.Exceptions;
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
        var newBook = new Book
        {
            Id = ++newId,
            Name = book.Name,
            Author = book.Author,
            ReleaseYear = book.ReleaseYear,
            Status = book.Status,
            GivenTo = String.IsNullOrEmpty(book.GivenTo) ? null : book.GivenTo,
            CreatedAt = DateTime.UtcNow
        };
        foreach (var genre in book.Genres)
        {
            newBook.AddGenre(genre);
        }
        
        Books.Add(newBook);
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
        book.AddGenre(genre);
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
    
    public void DeleteGenre(int bookId, GenresList genre, GenresList? newGenre = null)
    {
        var book = GetBook(bookId);
        if (!book.Genres.Contains(genre))
        {
            throw new KeyNotFoundException($"Genre {genre.ToString()} not found");
        }
        
        if (newGenre != null)
        {
            if (genre == newGenre.Value)
            {
                return;
            }
            if(!book.Genres.Contains(newGenre.Value))
                book.AddGenre(newGenre.Value);
        }
        book.RemoveGenre(genre);
    }

    public void DeleteAllGenres(int bookId, GenresList newGenre)
    {
        var book = GetBook(bookId);
        var genres = book.Genres;
        
        var lastGenre = genres.Last();
        
        foreach (var genre in genres)
        {
            if (genre == lastGenre)
            {
                book.SwapGenres(genre, newGenre);
                break;
            }
            book.RemoveGenre(genre);
        }
    }
}
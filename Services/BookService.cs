using GesHomeLibrary.Models;
using GesHomeLibrary.Interfaces;

namespace GesHomeLibrary.Services;

public class BookService: IBookService
{
    private readonly GenreParseService _genreParseService = new GenreParseService();
    private readonly StatusParseService _statusParseService = new StatusParseService();

    public List<Book> Books { get; set; } = new();
    
    public void AddBook(string book)
    {
        List<string> settings= book.Split(',').ToList();
        int genresCount = int.Parse(settings[4]);
        List<GenresList> genresList= new List<GenresList>();
        for (int i = 0; i < genresCount; i++)
        {
            genresList.Add(_genreParseService.GenreParseByName(settings[i+5]));
        }

        Books.Add(new Book
        {
            Id = int.Parse(settings[0]),
            Name = settings[1],
            Author = settings[2],
            ReleaseDate = DateTime.Parse(settings[3]),
            GenresCount = genresCount,
            Genres = genresList,
            Status = _statusParseService.StatusParseByName(settings[6 + genresCount]),
            CreatedAt = DateTime.UtcNow
        });
    }

    public void UpdateBookName(ref Book book, string  name)
    {
        book.Name = name;
    }

    public void UpdateBookAuthor(ref Book book, string author)
    {
        book.Author = author;
    }

    public void UpdateBookGenre(ref Book book, string genre)
    {
        var genres = book.Genres.ToList();
        genres.Add(_genreParseService.GenreParseByName(genre));
        book.Genres = genres;
        book.GenresCount++;
    }

    public void UpdateBookDate(ref Book book, DateTime date)
    {
        book.ReleaseDate = date;
    }

    public void UpdateBookStatus(ref Book book, string status, string? givenTo)
    {
        
        book.Status = _statusParseService.StatusParseByName(status);
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
    
    public  Book GetBook(int id)
    {
        return Books.FirstOrDefault(x => x.Id == id)!;
    }
    
    public void DeleteBook(Book book)
    {
        if (Books.Contains(book))
            Books.Remove(book);
    }

    public void DeleteAllBooks()
    {
        Books.Clear();
    }

    public void DeleteGenre(ref Book book, string genre)
    {
        var genres = book.Genres.ToList();
        genres.Remove(_genreParseService.GenreParseByName(genre));
        book.Genres = genres;
    }

    public void DeleteAllGenres(ref Book book)
    {
        book.Genres = new List<GenresList>();
    }
}
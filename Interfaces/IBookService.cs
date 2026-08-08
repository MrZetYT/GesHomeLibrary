using GesHomeLibrary.Models;

namespace GesHomeLibrary.Interfaces;

public interface IBookService
{
    void AddBook(string book);
    void DeleteBook(Book book);
    IEnumerable<Book> GetBooks();
    Book GetBook(int id);
    void DeleteAllBooks();
    void UpdateBookName(ref Book book, string name);
    void UpdateBookAuthor(ref Book book, string author);
    void UpdateBookGenre(ref Book book , string genre);
    void UpdateBookDate(ref Book book,  DateTime date);
    void UpdateBookStatus(ref Book book, string status, string? givenTo);
    void DeleteGenre(ref Book book, string genre);
    void DeleteAllGenres(ref Book book);
}
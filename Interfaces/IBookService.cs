using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;

namespace GesHomeLibrary.Interfaces;

public interface IBookService
{
    void AddBook(AddingBook book);
    void DeleteBook(int bookId);
    IEnumerable<Book> GetBooks();
    Book GetBook(int id);
    void DeleteAllBooks();
    void UpdateBookName(int bookId, string name);
    void UpdateBookAuthor(int bookId, string author);
    void UpdateBookGenre(int bookId, string genre);
    void UpdateBookDate(int bookId,  DateTime date);
    void UpdateBookStatus(int bookId, string status, string? givenTo);
    void DeleteGenre(int bookId, string genre);
    void DeleteAllGenres(int bookId);
}
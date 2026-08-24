using GesHomeLibrary.Models;
using GesHomeLibrary.Models.DTOs;

namespace GesHomeLibrary.Interfaces;

public interface IBookService
{
    void AddBook(AddingBook book);
    IEnumerable<Book> GetBooks();
    Book GetBook(int id);
    void UpdateBookName(int bookId, string name);
    void UpdateBookAuthor(int bookId, string author);
    void UpdateBookGenre(int bookId, GenresList genre);
    void UpdateBookDate(int bookId,  int date);
    void UpdateBookStatus(int bookId, StatusesList status, string? givenTo);
    void DeleteBook(int bookId);
    void DeleteAllBooks();
    void DeleteGenre(int bookId, GenresList genre);
    void DeleteAllGenres(int bookId);
    void ShowAllBooks(IEnumerable<Book> books);
}
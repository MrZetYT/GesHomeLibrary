using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class BookValidator
{
    public bool IsBookExist(int bookId, IEnumerable<Book> books)
    {
        if (books.FirstOrDefault(x => x.Id == bookId) == null)
        {
            return false;
        }
        return true;
    }
}
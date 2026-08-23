namespace GesHomeLibrary.Services;

public class BookValidator
{
    public bool ValidateGenresCount(int genresCount)
    {
        if(genresCount <= 0 || genresCount > 3)
            return false;
        return true;
    }
}
namespace GesHomeLibrary.Exceptions;

public class PossibleEmptyCollection: InvalidOperationException
{
    public PossibleEmptyCollection(string message) : base(message){}
    public PossibleEmptyCollection(string message, Exception inner) : base(message, inner){}
}
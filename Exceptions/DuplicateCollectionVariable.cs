namespace GesHomeLibrary.Exceptions;

public class DuplicateCollectionVariable : InvalidOperationException
{
    public DuplicateCollectionVariable(string message) : base(message) { }
    public DuplicateCollectionVariable(string message, Exception inner) : base(message, inner) { }
}
namespace Storage.Application.Exceptions;

public class InsufficientSpaceException : Exception
{
    public InsufficientSpaceException(string message) : base(message) { }
    public InsufficientSpaceException() : base("There is no such place") { }
}
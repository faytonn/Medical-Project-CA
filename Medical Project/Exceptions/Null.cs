public class IsNullException : Exception
{
    public IsNullException() : base("Input cannot be null")
    {
    }

    public IsNullException(string message) : base(message)
    {
    }

    public IsNullException(string message, Exception inner) : base(message, inner)
    {
    }
}
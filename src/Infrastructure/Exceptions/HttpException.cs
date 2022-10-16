namespace Infrastructure.Exceptions;

public abstract class HttpException : Exception
{
    public int HttpStatus { get; protected set; }

    public HttpException(string message, Exception? ex = null) : base(message, ex)
    {
        HttpStatus = 500;
    }   
}

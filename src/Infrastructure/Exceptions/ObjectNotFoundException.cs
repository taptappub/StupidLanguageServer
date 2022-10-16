using System;

namespace Infrastructure.Exceptions;

public class ObjectNotFoundException : HttpException
{
    public ObjectNotFoundException(string message, Exception? ex = null) : base(message, ex)
    {
        HttpStatus = 404;
    }   
}

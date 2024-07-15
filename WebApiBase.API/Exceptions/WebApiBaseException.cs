namespace WebApiBase.Exceptions;

[Serializable]
public class WebApiBaseException : Exception
{
    public int StatusCode { get; }

    public WebApiBaseException()
    {
    }

    public WebApiBaseException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public WebApiBaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

namespace Round_2.Application.Exceptions;
public class ServiceException : Exception
{
    public ServiceException(string message) : base(message)
    {
    }
}
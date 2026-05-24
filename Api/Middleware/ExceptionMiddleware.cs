using Round_2.Application.Exceptions;
using Round_2.Database.Exceptions;

public class ErrorResponse
{
    public string Message { get; set; } = "";
}

public class ExceptionMiddleware
{


    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private static Task WriteError(HttpContext context, int statusCode, string message)
{
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = statusCode;

    return context.Response.WriteAsJsonAsync(new ErrorResponse
    {
        Message = message
    });
}

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ServiceException ex)
        {
            await WriteError(context, 400, ex.Message);
        }
        catch (RepositoryException ex)
        {
            await WriteError(context, 409, ex.Message);
        }
        catch (Exception)
        {
            await WriteError(context, 500, "An unexpected error occurred");
        }
    }
}
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

    private static Task WriteError(HttpContext context, int statusCode, Exception ex)
    {
        Console.WriteLine(ex); // log the error so no silent failures for unknown errors.

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(new ErrorResponse
        {
            Message = ex.Message
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
            await WriteError(context, 400, ex);
        }
        catch (RepositoryException ex)
        {
            await WriteError(context, 409, ex);
        }
        catch (Exception e)
        {   
            Console.WriteLine(e);
            await WriteError(context, 500, new Exception("An unexpected error occurred"));
        }
    }
}
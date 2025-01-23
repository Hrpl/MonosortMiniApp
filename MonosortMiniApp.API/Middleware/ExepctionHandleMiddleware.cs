using MonosortMiniApp.Domain.Commons.Response;

namespace MonosortMiniApp.API.Middleware;

public class ExepctionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExepctionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExepctionHandleMiddleware> logger)
    {
        object? exceptionResponseBody = null;
        Exception? exception = null;
        int? statusCode = null;
        try
        {
            await _next(context);
        }
        catch (FormatException ex)
        {
            statusCode = 400;
            exceptionResponseBody = new BaseResponse<object?>(null, ex.Message); ;
            exception = ex;
        }
        catch (Exception ex)
        {
            statusCode = 400;
            exceptionResponseBody = new BaseResponse<object?>(null, ex.Message); ;
            exception = ex;
        }

        if (exception is not null)
        {
            context.Response.StatusCode = (int)statusCode!;
            logger.LogError(exception, "Вызвано исключение:");

            await context.Response.WriteAsJsonAsync(exceptionResponseBody!);
        }
    }
}

using EmployeeManagementApi.Exceptions;

namespace EmployeeManagementApi.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ApiException ex)
        {
            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            Console.WriteLine(ex.Message);

            await context.Response.WriteAsJsonAsync(new
            {
                message = "Unexpected error occured"
            });
        }
    }
}

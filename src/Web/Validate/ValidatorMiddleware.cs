using System.Globalization;

namespace Web.Validate;

public class ValidatorMiddleware
{
    private readonly RequestDelegate _next;
    public ValidatorMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}

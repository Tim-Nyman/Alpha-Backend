namespace WebApi.MiddleWares;


//Använde AI för att skapa delar av denna kod.
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "X-API-KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IConfiguration config)
    {
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var key))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing API Key");
            return;
        }

        var expectedKey = config["SecretKey:Default"];

        if (key != expectedKey)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}

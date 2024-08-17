using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Midlewares
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;
        public ErrorHandling(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandlingExceptionAsync(context, ex);
            }
        }

        private static async Task HandlingExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error  ="An error has occured while processing your request." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}

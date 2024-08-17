using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problemDetail = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "An error has occured while processing your request",
                Status = (int)HttpStatusCode.InternalServerError,

            };
            var exception = context.Exception;
            //var errorResult = // new { error = "An error has occured while processing your request" };
            context.Result = new ObjectResult(problemDetail)
            {
                StatusCode = 500,
            };
            context.ExceptionHandled = true;

        }
    }
}

using BuberDinner.Api.Http;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
            var firstError = errors[0];
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => (int)HttpStatusCode.Conflict,
                ErrorType.Validation => (int)HttpStatusCode.BadRequest,
                ErrorType.NotFound => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}

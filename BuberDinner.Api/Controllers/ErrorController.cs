using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            return Problem(title:ex?.Message, statusCode:400);
        }
    }
}

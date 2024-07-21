using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contract.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequestContract register)
        {
            var registerResult = _authenticationService.Register(register.FirstName, register.LastName, register.Email, register.Password);
            var authResponse = new AutResultContract(
                registerResult.Id,
                registerResult.FirstName,
                registerResult.LastName,
                registerResult.Email,
                registerResult.Token
            );
            return Ok(authResponse);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequestContract login) 
        {
            var loginResult = _authenticationService.Login(login.Email, login.Password);
            var authResponse = new AutResultContract(
               loginResult.Id,
               loginResult.FirstName,
               loginResult.LastName,
               loginResult.Email,
               loginResult.Token
           );
            return Ok(authResponse);
        }
    }
}

using BuberDinner.Api.Filters;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contract.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ErrorHandlingFilter]
    public class AuthenticationController : ApiController
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

            return registerResult.Match(
                result => Ok(MapAuthResult(result)),
                errs => Problem(errs)
            );
            //return registerResult.Match(
            //    authResult => Ok(MapAuthResult(authResult)),
            //    error => Problem(statusCode: (int)error.StatusCode, title:error.Message)
            //);
            //if(registerResult.IsT0)
            //{
            //    var authResult = registerResult.AsT0;
            //    var authResponse = new AutResultContract(
            //       authResult.Id,
            //       authResult.FirstName,
            //       authResult.LastName,
            //       authResult.Email,
            //       authResult.Token
            //    );
            //    return Ok(authResponse);
            //}

            //return Problem(statusCode:(int) HttpStatusCode.BadRequest,title: "Email already exists");
        }
        private static AutResultContract MapAuthResult(AuthenticationResult authResult)
        {
            return new AutResultContract(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token
            );
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequestContract login) 
        {
            var loginResult = _authenticationService.Login(login.Email, login.Password);
            if(loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: (int)HttpStatusCode.Unauthorized,
                    title: loginResult.FirstError.Description
                );
            }
            return loginResult.Match(
                result => Ok(MapAuthResult(result)),
                errs => Problem(errs)
            );
            //var authResponse = new AutResultContract(
           //    loginResult.Id,
           //    loginResult.FirstName,
           //    loginResult.LastName,
           //    loginResult.Email,
           //    loginResult.Token
           //);
           // return Ok(authResponse);
        }
    }
}

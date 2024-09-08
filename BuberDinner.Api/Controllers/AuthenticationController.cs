using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Commons;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contract.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ErrorHandlingFilter]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestContract registerModel)
        {
            //var command = new RegisterCommand(
            //    registerModel.FirstName, 
            //    registerModel.LastName, 
            //    registerModel.Email, 
            //    registerModel.Password
            //);
            var command = _mapper.Map<RegisterCommand>(registerModel);
            var registerResult = await _mediator.Send(command);

            return registerResult.Match(
                result => Ok(
                    //MapAuthResult(result)
                    _mapper.Map<AutResultContract>(result)
                    ),
                errs => Problem(errs)
            );
            
        }
        private static AutResultContract MapAuthResult(AuthenticationResult authResult)
        {
            return new AutResultContract(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
            );
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestContract login) 
        {
            //var loginQuery = new LoginQuery(login.Email, login.Password);
            var loginQuery = _mapper.Map<LoginQuery>(login);
            var loginResult = await _mediator.Send(loginQuery);
            //var loginResult = _autQueryService.Login(login.Email, login.Password);
            if(loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: (int)HttpStatusCode.Unauthorized,
                    title: loginResult.FirstError.Description
                );
            }
            return loginResult.Match(
                result => Ok(
                    //MapAuthResult(result)
                    _mapper.Map<AutResultContract>(result)
                ),
                errs => Problem(errs)
            );
         
        }
    }
}

using BuberDinner.Application.Commons.Errors;
using BuberDinner.Application.Services.Authentication.Commons;
using ErrorOr;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {
       
        ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);
    }
}

using BuberDinner.Application.Commons.Errors;
using ErrorOr;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Login(string Email, string Password);
        ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);
    }
}

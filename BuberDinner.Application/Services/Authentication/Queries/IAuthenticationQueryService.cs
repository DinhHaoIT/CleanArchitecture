using BuberDinner.Application.Commons.Errors;
using BuberDinner.Application.Services.Authentication.Commons;
using ErrorOr;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string Email, string Password);
      
    }
}

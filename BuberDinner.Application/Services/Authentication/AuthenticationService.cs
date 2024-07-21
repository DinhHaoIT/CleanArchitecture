using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResult Login(string Email, string Password)
        {
            return new AuthenticationResult(
                Guid.Empty,
                "FirstName",
                "LastName",
                Email,
                "token_"+Guid.NewGuid().ToString()
            );
        }

        public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                FirstName,
                LastName,
                Email,
                "token_"+Guid.NewGuid().ToString()
            );
        }
    }
}

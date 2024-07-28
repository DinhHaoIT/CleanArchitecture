using BuberDinner.Application.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTGenerator _jWTGenerator;

        public AuthenticationService(IJWTGenerator jWTGenerator)
        {
            _jWTGenerator = jWTGenerator;
        }

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
            Guid userId = Guid.NewGuid();
            string token = _jWTGenerator.GenerateToken(userId, FirstName,LastName);
            return new AuthenticationResult(
                userId,
                FirstName,
                LastName,
                Email,
                token
            );
        }
    }
}

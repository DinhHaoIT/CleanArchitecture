using BuberDinner.Application.Commons.Errors;
using BuberDinner.Application.Commons.Interfaces;
using BuberDinner.Application.Commons.Persistence;
using BuberDinner.Application.Services.Authentication.Commons;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJWTGenerator _jWTGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationQueryService(IJWTGenerator jWTGenerator, IUserRepository userRepository)
        {
            _jWTGenerator = jWTGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string Email, string Password)
        {
            //1. Validate users exists
            if(_userRepository.GetUserByEmail(Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
                //throw new Exception("User with given email does not exists");
            }
            //2. check password
            if (user.Password != Password)
            {
                //throw new Exception("Password is incorrect");
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jWTGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token
            );
        }

      
    }
}

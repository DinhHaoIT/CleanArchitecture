using BuberDinner.Application.Commons.Errors;
using BuberDinner.Application.Commons.Interfaces;
using BuberDinner.Application.Commons.Persistence;
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

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTGenerator _jWTGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IJWTGenerator jWTGenerator, IUserRepository userRepository)
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

        public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password)
        {
            //1. Check user existed
            if(_userRepository.GetUserByEmail(Email) is not null)
            {
                //throw new Exception("User with given email already exists");
                return Errors.User.DuplicateMail;
            }
            var user = new User
            {
                FirstName = FirstName,
                LastName =  LastName,
                Email = Email,
                Password = Password
            };
            _userRepository.AddUser(user);
            string token = _jWTGenerator.GenerateToken(user.Id, FirstName,LastName);
            return new AuthenticationResult(
                user.Id,
                FirstName,
                LastName,
                Email,
                token
            );
        }
    }
}

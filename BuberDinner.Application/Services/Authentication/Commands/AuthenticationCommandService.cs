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

namespace BuberDinner.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJWTGenerator _jWTGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationCommandService(IJWTGenerator jWTGenerator, IUserRepository userRepository)
        {
            _jWTGenerator = jWTGenerator;
            _userRepository = userRepository;
        }
        public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password)
        {
            //1. Check user existed
            if (_userRepository.GetUserByEmail(Email) is not null)
            {
                //throw new Exception("User with given email already exists");
                return Errors.User.DuplicateMail;
            }
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
            _userRepository.AddUser(user);
            string token = _jWTGenerator.GenerateToken(user.Id, FirstName, LastName);
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

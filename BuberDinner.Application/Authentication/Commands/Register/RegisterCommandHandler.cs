using BuberDinner.Application.Commons.Interfaces;
using BuberDinner.Application.Commons.Persistence;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using BuberDinner.Application.Authentication.Commons;
namespace BuberDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJWTGenerator _jWTGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJWTGenerator jWTGenerator, IUserRepository userRepository)
        {
            _jWTGenerator = jWTGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //1. Check user existed
            if (_userRepository.GetUserByEmail(request.Email) is not null)
            {
                //throw new Exception("User with given email already exists");
                return Errors.User.DuplicateMail;
            }
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };
            _userRepository.AddUser(user);
            string token = _jWTGenerator.GenerateToken(user.Id, request.FirstName, request.LastName);
            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}

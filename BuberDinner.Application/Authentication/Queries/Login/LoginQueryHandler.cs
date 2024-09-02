using BuberDinner.Application.Authentication.Commons;
using BuberDinner.Application.Commons.Interfaces;
using BuberDinner.Application.Commons.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJWTGenerator _jWTGenerator;
        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(IJWTGenerator jWTGenerator, IUserRepository userRepository)
        {
            _jWTGenerator = jWTGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            //1. Validate users exists
            if (_userRepository.GetUserByEmail(request.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
                //throw new Exception("User with given email does not exists");
            }
            //2. check password
            if (user.Password != request.Password)
            {
                //throw new Exception("Password is incorrect");
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jWTGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}

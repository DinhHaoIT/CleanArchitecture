using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Commons;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contract.Authentication;
using Mapster;

namespace BuberDinner.Api.Mapping
{
    public class AuthenticationMappingConfig:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequestContract, RegisterCommand>();
            config.NewConfig<LoginRequestContract, LoginQuery>();
            config.NewConfig<AuthenticationResult,AutResultContract>()
                .Map(des => des, src => src.User);
        }
    }
}

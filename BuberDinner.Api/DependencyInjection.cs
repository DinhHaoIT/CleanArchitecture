using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetails>();
            services.AddMappings();
            return services;
        }
    }
}

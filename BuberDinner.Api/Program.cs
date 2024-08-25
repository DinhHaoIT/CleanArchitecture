using BuberDinner.Api.Common.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    //builder.Services.AddControllers(opt=>opt.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetails>();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandling>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}



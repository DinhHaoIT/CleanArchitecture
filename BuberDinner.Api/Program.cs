using BuberDinner.Api.Filters;
using BuberDinner.Api.Midlewares;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    //builder.Services.AddControllers(opt=>opt.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandling>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}



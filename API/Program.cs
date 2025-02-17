using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.services;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        }
        );






//before the below written code we added the cors service to the service container this is important or we can say it is kind of a inmportant step to add cors service to the service container
var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
app.UseCors("AllowAngularApp");
// x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200,https://localhost:4200")
// ); 
//this is the solution to give api server access to the angular server in short giving communication line to both the server 
// also we allowed any header and any method to be used by the angular server and also we allowed the origin of the angular server to be used by the api server

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//Creates a disposable scope for accessing application services.
using var scope = app.Services.CreateScope();
// Gets the service provider from the created scope.
var services = scope.ServiceProvider;
try
{
    //Resolves an instance of the DataContext from the service provider.
    var context = services.GetRequiredService<DataContext>();
    //Asynchronously applies any pending database migrations.
    await context.Database.MigrateAsync();
    //Asynchronously seeds the database with user data.
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();

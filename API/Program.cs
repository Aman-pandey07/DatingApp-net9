using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
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


// Configure the HTTP request pipeline.
app.UseCors("AllowAngularApp"); 
// x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200,https://localhost:4200")
// ); 
//this is the solution to give api server access to the angular server in short giving communication line to both the server 
// also we allowed any header and any method to be used by the angular server and also we allowed the origin of the angular server to be used by the api server

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

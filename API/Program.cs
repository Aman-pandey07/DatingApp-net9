using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt=>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
app.UseCors(
    "AllowAngularApp");
    // x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200,https://localhost:4200")
    // ); 
//this is the solution to give api server access to the angular server in short giving communication line to both the server 
// also we allowed any header and any method to be used by the angular server and also we allowed the origin of the angular server to be used by the api server

app.MapControllers();

app.Run();

using System;
using Microsoft.AspNetCore.SignalR;
using API.Extensions;

namespace API.Entities;

public class AppUser
{
    public int Id {get; set;}
    public required string UserName { get; set; } 
    public   byte[] PasswordHash { get; set; } = [];
    //here  we define a password hash property to store the hash value for the user password in our database
    public  byte[] PasswordSalt { get; set; } = [];
    //here  we define a password salt property to store the salt value for the user password hash value in our database

    //after this above step we will pass a command to the terminal to create a migration for this new entity as * dotnet ef migration add UserEntityUpdated* 

    public DateOnly DateOfBirth { get; set; }

    public required string KnownAs { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public required string Gender { get; set; }

    public string? Introduction {get; set;}

    public string? Intrests {get; set;}

    public string? LookingFor {get; set;}

    public required string City { get; set; }

    public required string Country { get; set; }

    public List<Photo> Photos { get; set;} = [];

    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
}

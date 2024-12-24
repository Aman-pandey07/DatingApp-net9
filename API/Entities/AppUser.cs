using System;
using Microsoft.AspNetCore.SignalR;

namespace API.Entities;

public class AppUser
{
    public int Id {get; set;}
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    //here  we define a password hash property to store the hash value for the user password in our database
    public required byte[] PasswordSalt { get; set; }
    //here  we define a password salt property to store the salt value for the user password hash value in our database

    //after this above step we will pass a command to the terminal to create a migration for this new entity as * dotnet ef migration add UserEntityUpdated* 
}

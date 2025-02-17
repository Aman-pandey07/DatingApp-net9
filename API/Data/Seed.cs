using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext  context)
    {
        //this line checks that is there exist any pre existing user in our database already  and if it is then it exits the operation
        if(await context.Users.AnyAsync()) return;

        //this line reads all the data present in json file and store it in a variable
        var userData = await File.ReadAllBytesAsync("Data/UserSeedData.json");

        // "This line creates a JsonSerializerOptions object that makes property name comparisons during JSON serialization and deserialization case-insensitive."
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if(users == null) return;

        //here we are iterating over our users in the json data
        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            //here we are storing the username in lowercase in our database
            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes ("Pa$$w0rd")); 
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }

}

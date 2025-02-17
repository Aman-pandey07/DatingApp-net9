using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context,ITokenService tokenService) : BaseApiController 
// the DataContext is injected in the constructor of the AccountController to access the database 
{

    [HttpPost("register")] //this is a post request to register user and the end point is *api/account/register* it take account from the class name and remove the word Controller

    // public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
    // //Task<ActionResult<AppUser>> means 
    // // Task: Represents an asynchronous operation that will eventually return a result.
    // // ActionResult: A wrapper that provides a flexible way to return HTTP responses (e.g., success, error) from a controller in ASP.NET.
    // // AppUser: The actual type of data being returned (likely a user object).
    // {

    //     // we are checking if the username already exists in the database with the current username entered by the user
    //     if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");


        // using var hmac = new HMACSHA512(); 
        // //using statement is used to ensure that the object is disposed of when it goes out of scope.
        
        // var user = new AppUser
        // {
        //     UserName = registerDto.Username.ToLower(),
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     // we are converting the password to byte array and then hashing it
        //     PasswordSalt = hmac.Key 
        //     // this is the key that we will use to verify the password
        // };

        // context.Users.Add(user); // this step is done to add the user to the database
        // await context.SaveChangesAsync(); // this step is done to save the changes to the database

        // return new UserDto
        // {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };
    // }


    //this is a post request to login user and the end point is *api/account/login* it take account from the class name and remove the word Controller
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        //in this we made a login dto class to get the username and password from the user just like the register dto class
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName ==loginDto.Username.ToLower());
        //in above line we are checking if the username entered by the user is present in the database or not

        if(user == null) return Unauthorized("Invalid username");


    //here first we are creating a new instance of HMACSHA512 and passing the password salt of the user to it
        var hmac = new HMACSHA512(user.PasswordSalt);
        //then we are computing the hash of the password entered by the user and converting it to byte array and storing it in ComputeHash variable
        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

    //then we are comparing the hash of the password entered by the user with the hash of the password stored in the database
        for(int i = 0; i<ComputeHash.Length; i++)
        {
            if(ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }
    


    //in this method we are checking if the username already exists in the database to compare we comparing the username entered by the user with the username in the database for this we are converting both the username to lower case because in c# the string comparison is case sensitive // Bob =! bob
    private async Task<bool> UserExists(string username){

        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
   

}

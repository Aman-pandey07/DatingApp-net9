using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

//this is a service that we want to use with the  dependency injection so we need to set it up in the program class file
//Iconfiguration is used to access the appsettings.json file and the token key that we have stored in it
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        //we are creating a variable tokenKey and assining it to config["TokenKey"] this is used to access the token key from the appsettings.json file
        var tokenKey = config["TokenKey"] ?? throw new Exception("cannot acces token key from app settings");
        //we are checking if the token key is less than 64 characters then we are throwing an exception
        if(tokenKey.Length <64) throw new Exception("Token key needs to be longer than 64 characters");
        //here we areencoding the token key to byte array and storing it in key variable and SymmetricSecurityKey is used to encrypt and decrypt both the purpose
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //we are creating a list of claims and adding the username of the user to it
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier,user.UserName)
        };

        //we are creating a new instance of SigningCredentials and passing the key and the algorithm to it
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        //here we are creating a tokenDescriptor and passing the claims, expiry date and the credentials to it to pass the information through the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //the claimIdentity is used to pass the claims through the token
            Subject = new ClaimsIdentity(claims),
            //the expiry date is used to set the expiry date of the token
            Expires = DateTime.Now.AddDays(7),
            //the signing credentials is used to pass the credentials through the token
            SigningCredentials = creds
        };

        //we are creating a new instance of JwtSecurityTokenHandler 
        var tokenHandler = new JwtSecurityTokenHandler();

        //we are creating a token and passing the tokenDescriptor to it
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //we are writing the token and returning it
        return tokenHandler.WriteToken(token);

    }
}

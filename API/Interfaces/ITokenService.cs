using System;
using API.Entities;

namespace API.Interfaces;

// This interface is used to create a token for the user this is the part of the authentication process
//the file name of interface should start with I
public interface ITokenService
{
    //string here signifies that our token is a string
    //CreateToken is the method that will be used to create the token and we are passing the user as a parameter in this menthod 
    //this is jut a interface so we are not defining the body of the method here
    string CreateToken(AppUser user);
}

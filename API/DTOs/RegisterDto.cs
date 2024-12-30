using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

//we have created this file to create a data transfer object for the register method in the AccountController to transfer data from the client to the server which we were unable to do with the previous method as we were passing the data as parameters in the method of the account controller
public class RegisterDto
{
    [Required]
    public  string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(8,MinimumLength =4)]
    public  string Password { get; set; } = string.Empty;

}
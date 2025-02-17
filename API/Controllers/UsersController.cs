using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// we removed thsis as we made a common base controller
// [ApiController]
// [Route("api/[controller]")]  // api/users - end point for the http request (this take class name prior to Controller and remove the word Controller automatically)

[Authorize]// this is placed here means that the user must be authorized to access the data
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    

    // [AllowAnonymous]// this is used to make sure that the user is not authorized to access the data
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users =  await userRepository.GetMembersAsync();

        return Ok(users);
    }

    // [Authorize]
    [HttpGet("{username}")] // api/users/3
    public async Task<ActionResult<MemberDto>> GetUsers(string  username) 
    {
        var user =  await userRepository.GetMemberAsync(username);
        
        if(user == null) return NotFound();
        return user;
    }
   
}

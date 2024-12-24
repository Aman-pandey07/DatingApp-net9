using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// we removed thsis as we made a common base controller
// [ApiController]
// [Route("api/[controller]")]  // api/users - end point for the http request (this take class name prior to Controller and remove the word Controller automatically)

[Authorize]// this is placed here means that the user must be authorized to access the data
public class UsersController(DataContext context) : BaseApiController
{
    

    [AllowAnonymous]// this is used to make sure that the user is not authorized to access the data
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users =  await context.Users.ToListAsync();
        
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:int}")] // api/users/3
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(int  id) 
    {
        var user =  await context.Users.FindAsync(id);
        
        if(user == null) return NotFound();
        return Ok(user);
    }
   
}

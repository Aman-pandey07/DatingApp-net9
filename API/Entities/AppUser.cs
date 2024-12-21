using System;
using Microsoft.AspNetCore.SignalR;

namespace API.Entities;

public class AppUser
{
    public int Id {get; set;}
    public required string UserName { get; set; }
}

using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//we have created this file as a result to achive inheritance in the controllers like every controller we create extends the controller base class and also use a ApiController attribute and Route attribute so we created a common controller which will do all this and all other controller will extend this controller


[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{

}

using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace TestTask.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) 
    {
        _auth = auth;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signin")]
    public async Task<IActionResult> SignIn(LoginUserModel model)
    {
        bool isSuccessful = await _auth.TrySignIn(model);
        if (isSuccessful)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signup")]
    public async Task<IActionResult> SignUp(LoginUserModel model)
    {
        bool isSuccessful = await _auth.TrySignUp(model);
        if(isSuccessful)
        {
            return Ok();
        }
        return BadRequest(); 
    }
    
    
}





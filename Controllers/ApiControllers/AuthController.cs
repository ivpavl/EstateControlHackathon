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
    public async Task<IActionResult> SignIn([FromBody]LoginUserModel model)
    {
        bool isSuccessful = await _auth.TrySignIn(model);
        if (isSuccessful)
        {
            return Ok();
        }
        return StatusCode(StatusCodes.Status401Unauthorized);
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
        return StatusCode(StatusCodes.Status400BadRequest); 
    }
    
    [HttpPost]
    [Authorize]
    [Route("signout")]
    public async Task<IActionResult> SignOutUser()
    {
        await _auth.SignOutUser();
        return Ok(); 
    }
    
}





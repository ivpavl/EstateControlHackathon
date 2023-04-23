using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Data;
using TestTask.Data.Static;
using TestTask.Data.Services;
using TestTask.Data.Extensions;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace TestTask.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    // private readonly AppDbContext _context;
    private readonly IUserService _auth;

    public AuthController(IUserService auth) 
    {
        // _context = context;
        _auth = auth;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signin")]
    public async Task<IActionResult> Login([FromBody]LoginUserModel model)
    {
        var (user, isExist) = _auth.Login(model);
        if(isExist)
        {
            HttpClient client = new HttpClient();
            await _auth.SignInUsingToken(user);
            return Ok();
        }
        return NotFound(); 
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signup")]
    public async Task<IActionResult> Register(LoginUserModel model)
    {
        var (user, isSuccessful) = await _auth.Register(model);
        if(isSuccessful)
        {
            HttpClient client = new HttpClient();
            await _auth.SignInUsingToken(user);
            return Ok();
        }
        return NotFound(); 
    }
    
    
}





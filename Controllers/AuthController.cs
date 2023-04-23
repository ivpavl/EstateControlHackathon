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
        // user = new UserModel() {Name = model.Name, Password = model.Password};
        if(isExist)
        {
            HttpClient client = new HttpClient();
            await _auth.SignInUsingToken(user);
            // var response = new
            // {
            //     access_token = token,
            //     token_type = "Bearer"
            // };    
            // Response.Headers.Add("Authorization", "Bearer " + token);
            // HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>().HttpContext.SignInAsync()
            return Ok();
        }
        return NotFound(); 
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signup")]
    public async Task<IActionResult> Register([FromBody]LoginUserModel model)
    {
        var (user, isSuccessful) = await _auth.Register(model);
        if(isSuccessful)
        {
            HttpClient client = new HttpClient();
            await _auth.SignInUsingToken(user);
            // var response = new
            // {
            //     access_token = token,
            //     token_type = "Bearer"
            // };    
            // Response.Headers.Add("Authorization", "Bearer " + token);
            // HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>().HttpContext.SignInAsync()
            // return Ok(response);
            return Ok();
        }
        return NotFound(); 
    }
    
    // [HttpPost]
    // [AllowAnonymous]
    // public async Task<IActionResult> Login([FromBody]LoginUserModel model)
    // {
    //     var (user, isExist) = _auth.Login(model);
    //     // user = new UserModel() {Name = model.Name, Password = model.Password};
    //     if(isExist)
    //     {
    //         HttpClient client = new HttpClient();
    //         var token = await _auth.GenerateJwtToken(user);
    //         var response = new
    //         {
    //             access_token = token,
    //             token_type = "Bearer"
    //         };    
    //         // Response.Headers.Add("Authorization", "Bearer " + token);
    //         // HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>().HttpContext.SignInAsync()
    //         return Ok(response);
    //     }
    //     return NotFound(); 
    // }



    // [HttpGet]
    // [Authorize]
    // public async Task<IActionResult> Data()
    // {
    //     List<UserModel> UserList = new List<UserModel>(){
    //         new UserModel() {Name = "vasiliy", Password = "123"},
    //         new UserModel() {Name = "vas", Password = "1"}
    //     };
    //     await _context.Users.AddRangeAsync(UserList);

    //     var EstateList = new List<EstateModel>(){
    //         new EstateModel(){Name = "Hello", User = UserList[0], StatusId = 0},
    //         new EstateModel(){Name = "Hello2", User = UserList[0], StatusId = 0},
    //         new EstateModel(){Name = "Hello3", User = UserList[1], StatusId = 0}
    //     };
    //     await _context.Estates.AddRangeAsync(EstateList);

    //     await _context.SaveChangesAsync();
    //     return Ok(new {message="Added successfully"});

    //     // var (user, isExist) = _auth.Login(model);
    //     // if(isExist)
    //     // {
    //     //     HttpClient client = new HttpClient();
    //     //     var token = await _auth.GenerateJwtToken(user);
    //     //     var response = new
    //     //     {
    //     //         access_token = token,
    //     //         token_type = "Bearer"
    //     //     };
    //     //     Response.Headers.Add("Authorization", "Bearer " + token);
    //     //     return Ok(response);
    //     // }
    //     // else
    //     // {
    //     //     return NotFound(); 
    //     // }
    // }
    
}





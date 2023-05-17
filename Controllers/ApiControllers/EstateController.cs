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
using System.Security.Claims;

namespace TestTask.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EstateController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IAuthService _user;
    private readonly IEstateService _estate;
    private readonly IWebHostEnvironment _environment;

    public EstateController(AppDbContext context, IAuthService user, IEstateService estate, IWebHostEnvironment environment) 
    {
        _context = context;
        _user = user;
        _estate = estate;
        _environment = environment;
    }

    [HttpGet]
    [Route("getlist")]
    public async Task<IActionResult> GetEstateList()
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        List<EstateModel> userEstateList = _estate.GetEstatesListByUserId(userId);
        return Ok(new {userEstateList});
    }

    [HttpPost]
    [Route("getlist")]
    public async Task<IActionResult> GetEstateListByStatus(int statusId)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        List<EstateModel> userEstateList = _estate.GetEstatesListByUserId(userId, statusId);
        return Ok(new {userEstateList});
    }

    // [HttpPost]
    // [Route("")]
    // public async Task<IActionResult> AddEstate(EstateModel estate)
    // {
    //     var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    //     UserModel user = _user.GetUserById(userId);
    //     estate.User = user;

    //     await _estate.AddEstate(estate);

    //     return Ok();
    // }

    [HttpPost]
    [Route("remove")]
    public async Task<IActionResult> RemoveEstate(int estateId)
    {
        await _estate.RemoveEstateById(estateId);

        return Ok();
    }
    
    [HttpPost]
    [Route("editstatus")]
    public async Task<IActionResult> EditEstate(int estateId, int statusId)
    {
        await _estate.ChangeStatus(estateId, statusId);
        return Ok();
    }


    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddObject([FromForm] NewEstateModel model)
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            model.UserId = userId;
            await _estate.AddEstate(model);
            return Ok("Object added successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}





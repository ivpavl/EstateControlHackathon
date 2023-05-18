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
    private readonly IEstateService _estate;

    public EstateController(IEstateService estate) 
    {
        _estate = estate;
    }

    [HttpGet]
    [Route("getlist")]
    public IActionResult GetEstateList()
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userEstateList = _estate.GetEstatesListByUserId(userId);
            return Ok(new {userEstateList});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpDelete]
    [Route("remove")]
    public async Task<IActionResult> RemoveEstate(int estateId)
    {
        try
        {
            await _estate.RemoveEstateById(estateId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }
    
    [HttpPost]
    [Route("editstatus")]
    public async Task<IActionResult> EditEstate(int estateId, int statusId)
    {
        try
        {
            await _estate.ChangeStatus(estateId, statusId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
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
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }
}





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
public class BankingController : ControllerBase
{
    private readonly IBankingService _banking;

    public BankingController(IBankingService banking) 
    {
        _banking = banking;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddCard(CardModel card)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _banking.AddCard(card, Convert.ToInt32(userId));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpDelete]
    [Route("remove")]
    public async Task<IActionResult> RemoveUserCard()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _banking.RemoveCard(Convert.ToInt32(userId));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }
    
    [HttpPost]
    [Route("transfer")]
    public async Task<IActionResult> Transfer(string transferToUserName, int amout)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _banking.Transfer(Convert.ToInt32(userId), transferToUserName, amout);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpGet]
    [Route("info")]
    public IActionResult GetCardInfo()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            CardModel card = _banking.GetCardInfo(Convert.ToInt32(userId));
            return Ok(card);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost]
    [Route("addmoney")]
    public async Task<IActionResult> AddMoney(int amout)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _banking.AddMoney(Convert.ToInt32(userId), amout);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

}





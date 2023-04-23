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
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _banking.AddCard(card, Convert.ToInt32(userId));
        return Ok();
    }

    [HttpDelete]
    [Route("remove")]
    public async Task<IActionResult> RemoveUserCard()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _banking.RemoveCard(Convert.ToInt32(userId));
        return Ok();
    }
    
    [HttpPost]
    [Route("transfer")]
    public async Task<IActionResult> Transfer(string transferToUserName, int amout)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _banking.Transfer(Convert.ToInt32(userId), transferToUserName, amout);
        return Ok();
    }

    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetCardInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        CardModel card = await _banking.GetCardInfo(Convert.ToInt32(userId));
        if(card is not null) card.User = null;
        return Ok(card);
    }

    [HttpPost]
    [Route("addmoney")]
    public async Task<IActionResult> AddMoney(int amout)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _banking.AddMoney(Convert.ToInt32(userId), amout);
        return Ok();
    }

}





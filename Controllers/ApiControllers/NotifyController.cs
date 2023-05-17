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
public class NotifyController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IAuthService _user;
    private readonly IEstateService _estate;
    private readonly INotifyService _notify;

    public NotifyController(AppDbContext context, IAuthService user, IEstateService estate, INotifyService notify) 
    {
        _context = context;
        _user = user;
        _estate = estate;
        _notify = notify;
    }

    [HttpPost]
    [Route("subscribe")]
    public async Task<IActionResult> Subscribe(string subscriberName)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _notify.SubscribeUser(userId, subscriberName);
        return Ok();
    }
    [HttpGet]
    [Route("getnotifications")]
    public async Task<IActionResult> GetNotifications()
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        List<string> notifications = await _notify.GetNotificaiton(userId);
        return Ok(notifications);
    }

    [HttpPost]
    [Route("unsubscribe")]
    public async Task<IActionResult> UnSubscribe(string subscriberName)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _notify.UnsubscribeUser(userId, subscriberName);
        return Ok();
    }

    [HttpPost]
    [Route("sendnotification")]
    public async Task<IActionResult> SendNotification(string message)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _notify.SendNotification(userId, message);
        return Ok();
    }

}





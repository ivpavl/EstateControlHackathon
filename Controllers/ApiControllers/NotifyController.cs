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
    private readonly INotifyService _notify;

    public NotifyController(INotifyService notify) 
    {
        _notify = notify;
    }

    [HttpPost]
    [Route("subscribe")]
    public async Task<IActionResult> Subscribe(string subscriberName)
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _notify.SubscribeUser(userId, subscriberName);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }
    [HttpGet]
    [Route("getnotifications")]
    public IActionResult GetNotifications()
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var notifications = _notify.GetNotificaiton(userId);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpGet]
    [Route("getsubscribers")]
    public IActionResult GetSubscribers()
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var notifications = _notify.GetUserSubscribers(userId);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost]
    [Route("unsubscribe")]
    public async Task<IActionResult> UnSubscribe(string subscriberName)
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _notify.UnsubscribeUser(userId, subscriberName);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost]
    [Route("sendnotification")]
    public async Task<IActionResult> SendNotification(string message)
    {
        try
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _notify.SendNotification(userId, message);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

}





using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Data.Static;
using TestTask.Data.Services;
using TestTask.Data.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace TestTask.Controllers;

[Authorize]
public class HomeController : Controller
{
    IBankingService _banking;
    INotifyService _notification;
    public HomeController(IBankingService banking, INotifyService notification)
    {
        _banking = banking;
        _notification = notification;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult AddEstate()
    {
        return View();
    }
    public IActionResult MyCabinet()
    {
        return View();
    }
    public IActionResult Notification()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return View(_notification.GetUserSubscribers(Convert.ToInt32(userId)));
    }
    public IActionResult AddCard()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return View(_banking.GetCardInfo(Convert.ToInt32(userId)));
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Estate()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

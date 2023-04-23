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
    private readonly ILogger<HomeController> _logger;
    // private readonly ISession _session;

    public HomeController(
        ILogger<HomeController> logger
        // IHttpContextAccessor httpContextAccessor
        )
    {
        _logger = logger;
        // _session = httpContextAccessor.HttpContext.Session;

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
    public IActionResult MyCab()
    {
        return View();
    }
    public IActionResult Notification()
    {
        return View();
    }
    public IActionResult AddCard()
    {
        return View();
    }
    public IActionResult PaymentHistory()
    {
        return View();
    }
    public IActionResult AddUnit()
    {
        return View();
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return View(new UserModel(){Id=1, Name="2", Password = "3"});
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

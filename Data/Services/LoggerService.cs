using TestTask.Models;
using TestTask.Data.Static;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestTask.Data.Services;
public class LoggerService : ILoggerService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public LoggerService(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public Task SendNotification(string message)
    {
        throw new NotImplementedException();
    }

    public Task SubscribeUser(string userName)
    {
        throw new NotImplementedException();
    }

    public Task UnsubscribeUser(string userName)
    {
        throw new NotImplementedException();
    }
}

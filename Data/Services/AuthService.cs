using TestTask.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TestTask.Data.Services;
public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public AuthService(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    private async Task SignInUser(UserModel user)
    { 
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await _httpContext.HttpContext.SignInAsync(principal);
        
    }

        public async Task<bool> TrySignIn(LoginUserModel user)
        {
            UserModel? userToAuthenticate = _context.Users.FirstOrDefault(m => m.Name == user.Name && m.Password == user.Password);
            
            if(userToAuthenticate is not null)
            {
                await SignInUser(userToAuthenticate!);
                return true;
            }
            return false;
        }

        public async Task<bool> TrySignUp(LoginUserModel user)
        {
                try
                {
                    UserModel? userToAuthenticate = _context.Users.FirstOrDefault(m => m.Name == user.Name);
                    if (userToAuthenticate is not null)
                        return false;

                    var newUser = new UserModel { Name = user.Name, Password = user.Password };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    await SignInUser(newUser);

                    return true;
                }
                catch
                {
                    return false;
                }
        }

        public UserModel GetUserById(int Id)
        {
            UserModel user = _context.Users.FirstOrDefault(user => user.Id == Id);
            if(user is not null)
            {
                return user;
            }
            return null!;
        }

}

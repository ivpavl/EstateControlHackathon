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
public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public UserService(AppDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    public async Task SignInUsingToken(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var identity = new ClaimsIdentity(claims, "jwt");
        var principal = new ClaimsPrincipal(identity);
        await _httpContext.HttpContext.SignInAsync(principal);
        
        // return encodedJwt;

    }

        public (UserModel, bool) Login(LoginUserModel user)
        {
            UserModel trueUser = _context.Users.FirstOrDefault(m => m.Name == user.Name && m.Password == user.Password)!;
            if(trueUser is not null)
                return (trueUser, true);

            return (null, false)!;
        }
        public async Task<(UserModel, bool)> Register(LoginUserModel user)
        {
            UserModel newlyAddedUser;
            try
            {
                var isUserExist = _context.Users.FirstOrDefault(u => u.Name == user.Name)!;
                if(isUserExist is not null)
                    throw new Exception("User already registered");
                _context.Users.Add(new UserModel(){Name = user.Name, Password = user.Password});
                await _context.SaveChangesAsync();
                newlyAddedUser = _context.Users.FirstOrDefault(u => u.Name == user.Name)!;
            }
            catch
            {
                return (null, false);
            }
            return (newlyAddedUser, true);
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

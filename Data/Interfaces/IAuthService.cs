using TestTask.Models;

namespace TestTask.Data.Services;
public interface IAuthService
{
        Task<bool> TrySignIn(LoginUserModel user);
        Task<bool> TrySignUp(LoginUserModel user);
        Task SignOutUser();
}

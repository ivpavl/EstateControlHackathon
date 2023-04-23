using TestTask.Models;

namespace TestTask.Data.Services;
public interface IUserService
{
        Task SignInUsingToken(UserModel user);
        (UserModel, bool) Login(LoginUserModel user);
        Task<(UserModel, bool)> Register(LoginUserModel user);
        UserModel GetUserById(int Id);

}

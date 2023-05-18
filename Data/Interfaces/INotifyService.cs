using TestTask.Models;

namespace TestTask.Data.Services;
public interface INotifyService
{

        Task SubscribeUser(int userId,string subscriberName);
        Task UnsubscribeUser(int userId, string subscriberName);
        Task SendNotification(int userId, string message);
        IEnumerable<string> GetNotificaiton(int userId);
        IEnumerable<SubscribedUser> GetUserSubscribers(int userId);

}

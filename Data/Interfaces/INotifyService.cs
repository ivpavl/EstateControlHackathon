using TestTask.Models;

namespace TestTask.Data.Services;
public interface INotifyService
{

        Task SubscribeUser(int userId,string subscriberName);
        Task UnsubscribeUser(int userId, string subscriberName);
        Task SendNotification(int userId, string message);
        Task<List<string>> GetNotificaiton(int userId);

}

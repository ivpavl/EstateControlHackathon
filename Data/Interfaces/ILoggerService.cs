using TestTask.Models;

namespace TestTask.Data.Services;
public interface ILoggerService
{

        Task SubscribeUser(string userName);
        Task UnsubscribeUser(string userName);
        Task SendNotification(string message);


}

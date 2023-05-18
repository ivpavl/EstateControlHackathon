using TestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Data.Services;
public class NotifyService : INotifyService
{
    private readonly AppDbContext _context;
    public NotifyService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<string> GetNotificaiton(int userId)
    {
        IEnumerable<NotificationModel> allUserNotifications = _context.Notifications.Where(n => n.UserId == userId);
        IEnumerable<string> messages = allUserNotifications.Select(n => n.Message);

        return messages;
    }
    public IEnumerable<SubscribedUser> GetUserSubscribers(int userId)
    {
        IEnumerable<SubscribedUser> subscribedUsers = _context.Users
            .Include(u => u.Subscribers)
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == userId)?.Subscribers!;
            foreach (var sub in subscribedUsers)
                sub.User = null; //So that cycles don't occur. (Like User.Subscribers.User.Subscribers ...)

        return subscribedUsers;
    }

    public async Task SendNotification(int userId, string message)
    {
        IEnumerable<SubscribedUser> subscribedUsers = GetUserSubscribers(userId);

        foreach(SubscribedUser sub in subscribedUsers)
        {
            _context.Notifications.Add(new NotificationModel(){
                UserId = sub.SubscriberId,
                Message = message
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task SubscribeUser(int userId, string subscriberName)
    {
        var user = _context.Users
            .Include(u => u.Subscribers)
            .FirstOrDefault(u => u.Id == userId);

        var subscriber = _context.Users
            .FirstOrDefault(u => u.Name == subscriberName);

        if(user is null || subscriber is null)
            throw new Exception("User or subscriber not found");
        
        user.Subscribers!.Add(new SubscribedUser(){
            UserId = userId,
            SubscriberName = subscriber.Name,
            SubscriberId = subscriber.Id
        });

        await _context.SaveChangesAsync();
    }

    public async Task UnsubscribeUser(int userId, string subscriberName)
    {
        var removeFromUser = await _context.Users
            .Include(u => u.Subscribers)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (removeFromUser == null)
            throw new Exception("User not found");

        var userToRemove = removeFromUser.Subscribers?.FirstOrDefault(s => s.SubscriberName == subscriberName);

        if (userToRemove == null)
            throw new Exception("Subscriber not found");

        _context.SubscribedUsers.Remove(userToRemove);
        await _context.SaveChangesAsync();
    }
}
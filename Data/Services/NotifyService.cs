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
using Microsoft.EntityFrameworkCore;

namespace TestTask.Data.Services;
public class NotifyService : INotifyService
{
    private readonly AppDbContext _context;

    public NotifyService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<string>> GetNotificaiton(int userId)
    {
        List<NotificationModel> notifications = _context.Notifications
            .Where(n => n.UserId == userId)
            .ToList();
        _context.Notifications.RemoveRange(notifications);


        List<string> messages = notifications.Select(n => n.Message).ToList();
        return messages;
    }

    public async Task SendNotification(int userId, string message)
    {
        List<int> subscriberIds = _context.SubscribedUsers
            .Where(su => su.Id == userId)
            .Select(su => su.UserId)
            .ToList();

        foreach(int id in subscriberIds)
        {
            _context.Notifications.Add(new NotificationModel(){
                UserId = id,
                Message = message
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task SubscribeUser(int userId, string subscriberName)
    {
        UserModel user = _context.Users
            .Include(u => u.Subscribers)
            ?.FirstOrDefault(u => u.Id == userId)!;

        UserModel subscriber = _context.Users
            .FirstOrDefault(u => u.Name == subscriberName)!;
        if(user is null || subscriber is null)
            throw new Exception("User or subscriber not found");
        
        user.Subscribers.Add(new SubscribedUser(){
            UserId = userId,
            SubscriberId = subscriber.Id
        });
        await _context.SaveChangesAsync();
    }

    public async Task UnsubscribeUser(int userId, string subscriberName)
    {
        List<SubscribedUser> userSubscribes = _context.Users
            .Include(u => u.Subscribers)
            .FirstOrDefault(u => u.Id == userId)?.Subscribers.ToList()!;

        UserModel subscribedUser = _context.Users
            .FirstOrDefault(u => u.Name == subscriberName)!;

        if(userSubscribes is null || subscribedUser is null)
            throw new Exception("User or subscriber not found");
            
        SubscribedUser userSubscribe = userSubscribes.FirstOrDefault(s => s.UserId == subscribedUser.Id)!;

        if (userSubscribe != null) 
        {
            userSubscribes.Remove(userSubscribe);
            await _context.SaveChangesAsync();
        }

    }
}
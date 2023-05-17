using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;


namespace TestTask.Data;
public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users {get;set;}
    public DbSet<EstateModel> Estates {get;set;}
    public DbSet<CardModel> Cards {get;set;}
    public DbSet<SubscribedUser> SubscribedUsers {get;set;} = null!;
    public DbSet<NotificationModel> Notifications {get;set;}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
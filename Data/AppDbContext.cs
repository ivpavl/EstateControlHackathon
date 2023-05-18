using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;


namespace TestTask.Data;
public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users {get;set;} = null!;
    public DbSet<EstateModel> Estates {get;set;} = null!;
    public DbSet<CardModel> Cards {get;set;} = null!;
    public DbSet<SubscribedUser> SubscribedUsers {get;set;} = null!;
    public DbSet<NotificationModel> Notifications {get;set;} = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
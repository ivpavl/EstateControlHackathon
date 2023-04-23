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
        // Database.EnsureDeleted();
        Database.EnsureCreated();
        // List<UserModel> UserList = new List<UserModel>(){
        //     new UserModel() {Name = "vasiliy", Password = "123"},
        //     new UserModel() {Name = "vas", Password = "1"}
        // };
        // Users.AddRangeAsync(UserList);
        // // List<CardModel> cards = new List<CardModel>()
        // // {
        // //     new CardModel() {Name="Card1", Number = "4444 1144", CVV = "2", Date = "ds", User = UserList[0]},
        // //     new CardModel() {Name="Card1", Number = "4444 1144", CVV = "2", Date = "ds", User = UserList[1]},
        // //     new CardModel() {Name="Card1", Number = "4444 1144", CVV = "2", Date = "ds", User = UserList[1]}
        // // };


        // // Cards.AddRangeAsync(cards);

        // var EstateList = new List<EstateModel>(){
        //     new EstateModel(){User = UserList[0], StatusId = 0, Address = "as", Price = 25, 
        //     RoomsNum = 2, Photo = "cd6a8a26-ba2b-4d54-a163-d7b155df8d31.png", Notes = "sa"},
        //     new EstateModel(){User = UserList[1], StatusId = 0, Address = "SSSS", Price = 25, 
        //     RoomsNum = 2, Photo = "cd6a8a26-ba2b-4d54-a163-d7b155df8d31.png", Notes = "sa"},
        //     new EstateModel(){User = UserList[1], StatusId = 0, Address = "ASDSDA", Price = 25, 
        //     RoomsNum = 2, Photo = "cd6a8a26-ba2b-4d54-a163-d7b155df8d31.png", Notes = "sa"}
        // };

        // Estates.AddRangeAsync(EstateList);

        // SaveChangesAsync();
    }

}
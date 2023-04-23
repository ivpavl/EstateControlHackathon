using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data.Services;
public class BankingService : IBankingService
{
    private readonly AppDbContext _context;
    private readonly IUserService _user;

    public BankingService(AppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task AddCard(CardModel card, int userId)
    {
        try
        {
            UserModel user = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Id == userId)!;
            if(user is not null && user.Card is null)
            {
                user.Card = card;
                await _context.SaveChangesAsync();
            }
        }
        catch
        {
            throw new Exception("User do not exist, or card already added");
        }

    }

    public async Task RemoveCard(int userId)
    {
        try
        {
            UserModel user = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Id == userId)!;
            if(user is not null)
            {
                user.Card = null;
                await _context.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
    public async Task<CardModel> GetCardInfo(int userId)
    {
        try
        {
            UserModel user = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Id == userId)!;
            if(user is not null && user.Card is not null)
            {
                return user.Card;
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task Transfer(int userId, string transferToUserName, int amout)
    {
        try
        {
            UserModel userFrom = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Id == userId)!;
            UserModel userTo = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Name == transferToUserName)!;
            if(userFrom is not null && userTo is not null)
            {
                if(userFrom.Card?.VirtualMoney >= amout && userTo.Card is not null)
                {
                    userTo.Card.VirtualMoney += amout;
                    userFrom.Card.VirtualMoney -= amout;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Not enough money, or one of users does not have card");
                }
            }
        }
        catch
        {
            throw;
        }
    }
    public async Task AddMoney(int userId, int amout)
    {
        try
        {
            UserModel user = _user.GetUserById(userId);
            if(user is not null && user.Card is not null)
            {
                user.Card.VirtualMoney += amout;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Your card might not exist");
            }
        }
        catch
        {
            throw;
        }
    }

}

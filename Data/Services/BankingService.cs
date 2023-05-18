using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data.Services;
public class BankingService : IBankingService
{
    private readonly AppDbContext _context;

    public BankingService(AppDbContext context)
    {
        _context = context;
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
            CardModel card = _context.Cards.FirstOrDefault(card => card.UserId == userId)!;
            if(card is not null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
    public CardModel GetCardInfo(int userId)
    {
        UserModel user = _context.Users.Include(user => user.Card).FirstOrDefault(user => user.Id == userId)!;
        if(user is not null && user.Card is not null)
        {
            return user.Card;
        }
        throw new Exception("User or user card not found!");
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
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
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

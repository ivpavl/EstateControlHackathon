using TestTask.Models;

namespace TestTask.Data.Services;
public interface IBankingService
{
        Task AddCard(CardModel card, int userId);
        Task RemoveCard(int userId);
        Task Transfer(int userId, string transferToUserName, int amout);
        Task AddMoney(int userId, int amout);
        CardModel GetCardInfo(int userId);

}

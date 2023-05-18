using TestTask.Models;

namespace TestTask.Data.Services;
public interface IEstateService
{
        IEnumerable<EstateModel> GetEstatesListByUserId(int userId);
        Task AddEstate(NewEstateModel estate);
        Task RemoveEstateById(int estateId);
        Task ChangeStatus(int estateId, int statusId);

}

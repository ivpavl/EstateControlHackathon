using TestTask.Models;

namespace TestTask.Data.Services;
public interface IEstateService
{
        List<EstateModel> GetEstatesListByUserId(int userId);
        List<EstateModel> GetEstatesListByUserId(int userId, int statusId);
        Task AddEstate(NewEstateModel estate);
        Task<string> UploadImage(IFormFile photo);
        Task RemoveEstateById(int estateId);
        Task ChangeStatus(int estateId, int statusId);

}

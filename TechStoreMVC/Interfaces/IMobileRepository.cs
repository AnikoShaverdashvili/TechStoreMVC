using TechStoreMVC.Models;

namespace TechStoreMVC.Interfaces
{
    public interface IMobileRepository
    {
        Task<IEnumerable<Mobile>> GetAllMobile();
        Task<Mobile>GetMobileById(int id);
        Task<Mobile> GetByIdAsyncNoTracking(int id);
        bool Add(Mobile mobile);
        bool Update(Mobile mobile);
        bool Delete(Mobile mobile);
        bool Save();



    }
}

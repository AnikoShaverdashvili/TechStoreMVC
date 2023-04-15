using TechStoreMVC.Models;

namespace TechStoreMVC.Interfaces
{
    public interface ILaptopRepository
    {
        Task<IEnumerable<Laptop>> GetLaptop();
        Task<Laptop> GetLaptopById(int id);
        Task<Laptop> GetByIdAsyncNoTracking(int id);
        bool Delete(Laptop laptop);
        bool Add(Laptop laptop);
        bool Update(Laptop laptop); 
        bool Save();

    }
}

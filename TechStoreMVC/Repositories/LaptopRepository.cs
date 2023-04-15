using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Data;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;

namespace TechStoreMVC.Repositories
{
    public class LaptopRepository:ILaptopRepository
    {
        private readonly ApplicationDbContext _context;

        public LaptopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Laptop laptop)
        {
            _context.Add(laptop);
            return Save();
        }

        public bool Delete(Laptop laptop)
        {
            _context.Remove(laptop);
            return Save();
        }

        public async Task<Laptop> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Laptops.Include(a=>a.Address).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Laptop>> GetLaptop()
        {
            return await _context.Laptops.ToListAsync();
        }

        public async Task<Laptop> GetLaptopById(int id)
        {
            return await _context.Laptops.Where(l => l.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool Update(Laptop laptop)
        {
            _context.Update(laptop);
            return Save();
        }
    }
}

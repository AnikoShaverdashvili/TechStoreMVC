using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Data;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;

namespace TechStoreMVC.Repositories
{
    public class MobileRepository : IMobileRepository
    {
        private readonly ApplicationDbContext _context;

        public MobileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Mobile mobile)
        {
            _context.Mobiles.Add(mobile);
            return Save();
        }

        public bool Delete(Mobile mobile)
        {
            _context.Remove(mobile);
            return Save();
        }

        public async Task<IEnumerable<Mobile>> GetAllMobile()
        {
            return await _context.Mobiles.ToListAsync();
        }

        public async Task<Mobile> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Mobiles.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Mobile> GetMobileById(int id)
        {
            return await _context.Mobiles.Include(i => i.Address).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool Update(Mobile mobile)
        {
            _context.Update(mobile);
            return Save();
        }
    }
}

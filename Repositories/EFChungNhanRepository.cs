using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
    public class EFChungNhanRepository
    {
        private readonly ApplicationDBContext _context;

        public EFChungNhanRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChungNhan>> GetAllAsync()
        {
            return await _context.ChungNhans.ToListAsync();
        }

        public async Task<ChungNhan> GetByIdAsync(string maChungNhan)
        {
            return await _context.ChungNhans.FindAsync(maChungNhan);
        }

        public async Task AddAsync(ChungNhan chungNhan)
        {
            _context.ChungNhans.Add(chungNhan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChungNhan chungNhan)
        {
            _context.ChungNhans.Update(chungNhan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maChungNhan)
        {
            var chungNhan = await GetByIdAsync(maChungNhan);
            if (chungNhan != null)
            {
                _context.ChungNhans.Remove(chungNhan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string maChungNhan)
        {
            return await _context.ChungNhans.AnyAsync(cn => cn.MaChungNhan == maChungNhan);
        }
    }
}

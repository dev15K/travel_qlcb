using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
    public class EFChuyenBayRepository
    {
        private readonly ApplicationDBContext _context;

        public EFChuyenBayRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChuyenBay>> GetAllAsync()
        {
            return await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .ToListAsync();
        }

        public async Task<ChuyenBay> GetByIdAsync(string maChuyenBay)
        {
            return await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .FirstOrDefaultAsync(cb => cb.MaChuyenBay == maChuyenBay);
        }

        public async Task AddAsync(ChuyenBay chuyenBay)
        {
            _context.ChuyenBays.Add(chuyenBay);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChuyenBay chuyenBay)
        {
            _context.ChuyenBays.Update(chuyenBay);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maChuyenBay)
        {
            var chuyenBay = await GetByIdAsync(maChuyenBay);
            if (chuyenBay != null)
            {
                _context.ChuyenBays.Remove(chuyenBay);
                await _context.SaveChangesAsync();
            }
        }
    }
}

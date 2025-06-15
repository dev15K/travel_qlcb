using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
    public class EFPhanCongRepository :IPhanCongRepository
    {
        private readonly ApplicationDBContext _context;
        public EFPhanCongRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PhanCong>> GetAllPhanCongsAsync()
        {
            return await _context.PhanCongs.ToListAsync();
        }
        public async Task<PhanCong> GetPhanCongByIdAsync(int id)
        {
            return await _context.PhanCongs.FindAsync(id);
        }
        public async Task<PhanCong> AddPhanCongAsync(PhanCong phanCong)
        {
            _context.PhanCongs.Add(phanCong);
            await _context.SaveChangesAsync();
            return phanCong;
        }
        public async Task<PhanCong> UpdatePhanCongAsync(int id, PhanCong phanCong)
        {
            if (id != phanCong.Id)
            {
                throw new ArgumentException("ID mismatch");
            }
            _context.Entry(phanCong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return phanCong;
        }
        public async Task<bool> DeletePhanCongAsync(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null)
            {
                return false;
            }
            _context.PhanCongs.Remove(phanCong);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}

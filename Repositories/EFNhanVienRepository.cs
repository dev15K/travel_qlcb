using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
    public class EFNhanVienRepository
    {
        private readonly ApplicationDBContext _context;
        public EFNhanVienRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _context.NhanViens.ToListAsync();
        }
        public async Task<NhanVien> GetByIdAsync(string maNhanVien)
        {
            return await _context.NhanViens.FindAsync(maNhanVien);
        }
        public async Task AddAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string maNhanVien)
        {
            var nhanVien = await GetByIdAsync(maNhanVien);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
                await _context.SaveChangesAsync();
            }
        }
    }
}

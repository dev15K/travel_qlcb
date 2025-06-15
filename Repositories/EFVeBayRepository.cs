using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
    public class EFVeBayRepository : IVeBayRepository
    {
        private readonly ApplicationDBContext _context;

        public EFVeBayRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VeBay>> GetAllVeBaysAsync()
        {
            return await _context.VeBays.ToListAsync();
        }

        public async Task<VeBay> GetVeBayByIdAsync(string maVeBay)
        {
            return await _context.VeBays.FindAsync(maVeBay);
        }

        public async Task<VeBay> AddVeBayAsync(VeBay veBay)
        {
            _context.VeBays.Add(veBay);
            await _context.SaveChangesAsync();
            return veBay;
        }

        public async Task<VeBay> UpdateVeBayAsync(string maVeBay, VeBay veBay)
        {
            if (maVeBay != veBay.MaVeBay)
            {
                return null;
            }
            _context.Entry(veBay).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return veBay;
        }

        public async Task<bool> DeleteVeBayAsync(string maVeBay)
        {
            var veBay = await GetVeBayByIdAsync(maVeBay);
            if (veBay == null)
            {
                return false;
            }
            _context.VeBays.Remove(veBay);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

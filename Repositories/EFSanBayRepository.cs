using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
	public class EFSanBayRepository : ISanBayRepository
	{
		private readonly ApplicationDBContext _context;

		public EFSanBayRepository(ApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<SanBay>> GetAllAsync()
		{
			return await _context.SanBays.ToListAsync();
		}

		public async Task<SanBay> GetByIdAsync(string maSanBay)
		{
			return await _context.SanBays.FindAsync(maSanBay);
		}

		public async Task AddAsync(SanBay sanBay)
		{
			await _context.SanBays.AddAsync(sanBay);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(SanBay sanBay)
		{
			_context.SanBays.Update(sanBay);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string maSanBay)
		{
			var sanBay = await GetByIdAsync(maSanBay);
			if (sanBay != null)
			{
				_context.SanBays.Remove(sanBay);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(string maSanBay)
		{
			return await _context.SanBays.AnyAsync(sb => sb.MaSanBay == maSanBay);
		}
	}
}

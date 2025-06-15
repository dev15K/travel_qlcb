using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Repositories
{
	public class EFMayBayRepository : IMayBayRepository
	{
		private readonly ApplicationDBContext _context;

		public EFMayBayRepository(ApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<MayBay>> GetAllAsync()
		{
			return await _context.MayBays.ToListAsync();
		}

		public async Task<MayBay> GetByIdAsync(string maMayBay)
		{
			return await _context.MayBays.FindAsync(maMayBay);
		}

		public async Task AddAsync(MayBay mayBay)
		{
			await _context.MayBays.AddAsync(mayBay);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(MayBay mayBay)
		{
			_context.MayBays.Update(mayBay);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string maMayBay)
		{
			var mayBay = await GetByIdAsync(maMayBay);
			if (mayBay != null)
			{
				_context.MayBays.Remove(mayBay);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(string maMayBay)
		{
			return await _context.MayBays.AnyAsync(mb => mb.MaMayBay == maMayBay);
		}
	}
}

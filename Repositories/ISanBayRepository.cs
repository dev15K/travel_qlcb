using QLCB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLCB.Repositories
{
	public interface ISanBayRepository
	{
		Task<IEnumerable<SanBay>> GetAllAsync();
		Task<SanBay> GetByIdAsync(string maSanBay);
		Task AddAsync(SanBay sanBay);
		Task UpdateAsync(SanBay sanBay);
		Task DeleteAsync(string maSanBay);
		Task<bool> ExistsAsync(string maSanBay);
	}
}

using QLCB.Models;

namespace QLCB.Repositories
{
	public interface IMayBayRepository
	{
		Task<IEnumerable<MayBay>> GetAllAsync();
		Task<MayBay> GetByIdAsync(string maMayBay);
		Task AddAsync(MayBay mayBay);
		Task UpdateAsync(MayBay mayBay);
		Task DeleteAsync(string maMayBay);
		Task<bool> ExistsAsync(string maMayBay);
	}
}

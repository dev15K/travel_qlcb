using QLCB.Models;

namespace QLCB.Repositories
{
    public interface IChuyenBayRepository
    {
        Task<IEnumerable<ChuyenBay>> GetAllAsync();
        Task<ChuyenBay> GetByIdAsync(string maChuyenBay);
        Task AddAsync(ChuyenBay chuyenBay);
        Task UpdateAsync(ChuyenBay chuyenBay);
        Task DeleteAsync(string maChuyenBay);
    }
}

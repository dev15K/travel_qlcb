using QLCB.Models;

namespace QLCB.Repositories
{
    public interface IChungNhanRepository
    {
        Task<IEnumerable<ChungNhan>> GetAllAsync();
        Task<ChungNhan> GetByIdAsync(string maChungNhan);
        Task AddAsync(ChungNhan chungNhan);
        Task UpdateAsync(ChungNhan chungNhan);
        Task DeleteAsync(string maChungNhan);
        Task<bool> ExistsAsync(string maChungNhan);
    }
}

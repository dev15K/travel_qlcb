using QLCB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLCB.Repositories
{
    public interface IPhanCongRepository
    {
        Task<IEnumerable<PhanCong>> GetAllPhanCongsAsync();
        Task<PhanCong> GetPhanCongByIdAsync(int id);
        Task<PhanCong> AddPhanCongAsync(PhanCong phanCong);
        Task<PhanCong> UpdatePhanCongAsync(int id, PhanCong phanCong);
        Task<bool> DeletePhanCongAsync(int id);
    }
}

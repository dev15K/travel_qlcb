using QLCB.Models;

namespace QLCB.Repositories
{
    public interface INhanVienRepository
    {
        public Task<IEnumerable<NhanVien>> GetAllAsync();
        public Task<NhanVien> GetByIdAsync(string maNhanVien);
        public Task AddAsync(NhanVien nhanVien);
        public Task UpdateAsync(NhanVien nhanVien);
        public Task DeleteAsync(string maNhanVien);
        public Task<bool> ExistsAsync(string maNhanVien);
    }
}

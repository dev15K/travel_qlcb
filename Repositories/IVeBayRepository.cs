using QLCB.Models;

namespace QLCB.Repositories
{
    public interface IVeBayRepository
    {
        Task<IEnumerable<VeBay>> GetAllVeBaysAsync();
        Task<VeBay> GetVeBayByIdAsync(string maVeBay);
        Task<VeBay> AddVeBayAsync(VeBay veBay);
        Task<VeBay> UpdateVeBayAsync(string maVeBay, VeBay veBay);
        Task<bool> DeleteVeBayAsync(string maVeBay);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")] // hoặc bỏ nếu chưa phân quyền
    public class PhanCongController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PhanCongController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy mã nhân viên từ tên đăng nhập
            var maNhanVien = User.Identity?.Name;

            if (string.IsNullOrEmpty(maNhanVien))
            {
                return Unauthorized(); // hoặc chuyển về trang login
            }

            var phanCongs = await _context.PhanCongs
                .Where(p => p.MaNhanVien == maNhanVien)
                .OrderByDescending(p => p.NgayPhanCong)
                .ToListAsync();

            return View(phanCongs);
        }
    }
}

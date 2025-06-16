using Microsoft.AspNetCore.Mvc;
using QLCB.Models;
using System.Diagnostics;
using QLCB.Models.ViewModels;

namespace QLCB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;
        
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string diemDi, string diemDen, DateTime? ngayDi)
        {
            var query = from cb in _context.ChuyenBays
                join sbDi in _context.SanBays on cb.MaSanBayDi equals sbDi.MaSanBay
                join sbDen in _context.SanBays on cb.MaSanBayDen equals sbDen.MaSanBay
                select new
                {
                    cb,
                    TenSanBayDi = sbDi.TenSanBay,
                    TenSanBayDen = sbDen.TenSanBay
                };

            /* Lọc theo điểm đi */
            if (!string.IsNullOrEmpty(diemDi))
            {
                query = query.Where(x => x.TenSanBayDi.Contains(diemDi));
            }

            /* Lọc theo điểm đến */
            if (!string.IsNullOrEmpty(diemDen))
            {
                query = query.Where(x => x.TenSanBayDen.Contains(diemDen));
            }

            /* Lọc theo ngày đi */
            if (ngayDi.HasValue)
            {
                var ngay = ngayDi.Value.Date;
                query = query.Where(x => x.cb.ThoiGianKhoiHanh.Date == ngay);
            }

            var danhSach = query.Select(x => new ChuyenBayViewModel
            {
                MaChuyenBay = x.cb.MaChuyenBay,
                TenChuyenBay = x.cb.TenChuyenBay,
                ThoiGianKhoiHanh = x.cb.ThoiGianKhoiHanh,
                ThoiGianDen = x.cb.ThoiGianDen,
                SoGhe = x.cb.SoGhe,
                TenSanBayDi = x.TenSanBayDi,
                TenSanBayDen = x.TenSanBayDen,
                VeBays = _context.VeBays
                    .Where(v => v.MaChuyenBay == x.cb.MaChuyenBay)
                    .Select(v => new VeBayViewModel
                    {
                        MaVeBay = v.MaVeBay,
                        NgayDatVe = v.NgayDatVe,
                        GiaVe = v.GiaVe,
                        MaHanhKhach = v.MaHanhKhach
                    }).ToList()
            }).ToList();

            return View(danhSach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

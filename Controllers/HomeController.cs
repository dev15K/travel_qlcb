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

        public IActionResult Index()
        {
            var danhSach = (from cb in _context.ChuyenBays
                join sbDi in _context.SanBays on cb.MaSanBayDi equals sbDi.MaSanBay
                join sbDen in _context.SanBays on cb.MaSanBayDen equals sbDen.MaSanBay
                select new ChuyenBayViewModel
                {
                    MaChuyenBay = cb.MaChuyenBay,
                    TenChuyenBay = cb.TenChuyenBay,
                    ThoiGianKhoiHanh = cb.ThoiGianKhoiHanh,
                    ThoiGianDen = cb.ThoiGianDen,
                    SoGhe = cb.SoGhe,
                    TenSanBayDi = sbDi.TenSanBay,
                    TenSanBayDen = sbDen.TenSanBay,
                    VeBays = _context.VeBays
                        .Where(v => v.MaChuyenBay == cb.MaChuyenBay)
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

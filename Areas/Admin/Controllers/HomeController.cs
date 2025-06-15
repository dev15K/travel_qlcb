using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới có thể truy cập
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 
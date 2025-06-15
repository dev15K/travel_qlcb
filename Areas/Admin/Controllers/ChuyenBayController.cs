using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuyenBayController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChuyenBayController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/ChuyenBay
        public async Task<IActionResult> Index()
        {
            var danhSachChuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .ToListAsync();

            return View(danhSachChuyenBay);
        }

        // GET: Admin/ChuyenBay/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .FirstOrDefaultAsync(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return NotFound();
            }

            return View(chuyenBay);
        }

        // GET: Admin/ChuyenBay/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MayBayList = await _context.MayBays.ToListAsync();
            ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
            ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();
            return View();
        }

        // POST: Admin/ChuyenBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChuyenBay chuyenBay)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidationErrors = ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .Select(m => new
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                // Load lại các dropdown list
                ViewBag.MayBayList = await _context.MayBays.ToListAsync();
                ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
                ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();

                TempData["ErrorMessage"] = "Thêm chuyến bay thất bại. Vui lòng kiểm tra lại thông tin.";
                return View(chuyenBay);
            }

            try
            {
                _context.ChuyenBays.Add(chuyenBay);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm chuyến bay mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "Thêm chuyến bay thất bại: " + ex.Message;
                // Load lại dropdown list để hiển thị lại form
                ViewBag.MayBayList = await _context.MayBays.ToListAsync();
                ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
                ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();

                return View(chuyenBay);
            }
        }

        // GET: Admin/ChuyenBay/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays.FindAsync(id);
            if (chuyenBay == null)
            {
                return NotFound();
            }

            ViewBag.MayBayList = await _context.MayBays.ToListAsync();
            ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
            ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();

            return View(chuyenBay);
        }

        // POST: Admin/ChuyenBay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ChuyenBay chuyenBay)
        {
            if (id != chuyenBay.MaChuyenBay)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ValidationErrors = ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .Select(m => new
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                ViewBag.MayBayList = await _context.MayBays.ToListAsync();
                ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
                ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();

                TempData["ErrorMessage"] = "Cập nhật chuyến bay thất bại. Vui lòng kiểm tra lại thông tin.";
                return View(chuyenBay);
            }

            try
            {
                _context.Update(chuyenBay);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật chuyến bay thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuyenBayExists(chuyenBay.MaChuyenBay))
                {
                    TempData["ErrorMessage"] = "Không tìm thấy chuyến bay để cập nhật.";
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "Cập nhật chuyến bay thất bại: " + ex.Message;
                ViewBag.MayBayList = await _context.MayBays.ToListAsync();
                ViewBag.SanBayDiList = await _context.SanBays.ToListAsync();
                ViewBag.SanBayDenList = await _context.SanBays.ToListAsync();

                return View(chuyenBay);
            }
        }

        // GET: Admin/ChuyenBay/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .FirstOrDefaultAsync(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return NotFound();
            }

            return View(chuyenBay);
        }

        // POST: Admin/ChuyenBay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chuyenBay = await _context.ChuyenBays.FindAsync(id);
            if (chuyenBay != null)
            {
                _context.ChuyenBays.Remove(chuyenBay);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa chuyến bay thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy chuyến bay để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ChuyenBayExists(string id)
        {
            return _context.ChuyenBays.Any(cb => cb.MaChuyenBay == id);
        }
    }
}
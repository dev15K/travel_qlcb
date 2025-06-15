using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Areas.Admin.Models;
using QLCB.Models;

namespace QLCB.Areas.Admin.Controllers // Sửa namespace để tránh xung đột
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)]
    public class SanBayController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SanBayController(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Đảm bảo _context không null
        }

        // GET: SanBay
        public async Task<IActionResult> Index()
        {
            var sanBays = await _context.SanBays.ToListAsync();
            return View(sanBays);
        }

        // GET: SanBay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(sanBay);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm sân bay mới thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu: " + ex.Message);
                    TempData["ErrorMessage"] = "Thêm sân bay thất bại: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Thêm sân bay thất bại. Vui lòng kiểm tra lại thông tin.";
            }
            return View(sanBay);
        }

        // GET: SanBay/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var sanBay = await _context.SanBays.FindAsync(id);
            if (sanBay == null)
                return NotFound();

            return View(sanBay);
        }

        // POST: SanBay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SanBay sanBay)
        {
            if (id != sanBay.MaSanBay)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanBay);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật sân bay thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanBayExists(sanBay.MaSanBay))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy sân bay để cập nhật.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Cập nhật sân bay thất bại: " + ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật sân bay thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(sanBay);
        }

        // GET: SanBay/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var sanBay = await _context.SanBays
                .FirstOrDefaultAsync(m => m.MaSanBay == id);
            if (sanBay == null)
                return NotFound();

            return View(sanBay);
        }

        // POST: SanBay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sanBay = await _context.SanBays.FindAsync(id);
            if (sanBay != null)
            {
                _context.SanBays.Remove(sanBay);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa sân bay thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy sân bay để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SanBayExists(string id)
        {
            return _context.SanBays.Any(e => e.MaSanBay == id);
        }
    }
}

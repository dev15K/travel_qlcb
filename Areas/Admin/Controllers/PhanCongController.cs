using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanCongController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PhanCongController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/PhanCong
        public async Task<IActionResult> Index()
        {
            var list = await _context.PhanCongs.ToListAsync();
            return View(list);
        }

        // GET: Admin/PhanCong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PhanCong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhanCong model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm phân công mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Thêm phân công thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(model);
        }

        // GET: Admin/PhanCong/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null) return NotFound();

            return View(phanCong);
        }

        // POST: Admin/PhanCong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhanCong model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật phân công thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PhanCongs.Any(p => p.Id == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy phân công để cập nhật.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật phân công thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(model);
        }

        // GET: Admin/PhanCong/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null) return NotFound();

            return View(phanCong);
        }

        // POST: Admin/PhanCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong != null)
            {
                _context.PhanCongs.Remove(phanCong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa phân công thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy phân công để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

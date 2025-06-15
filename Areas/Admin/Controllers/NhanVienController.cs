using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NhanVienController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NhanVienController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/NhanVien
        public async Task<IActionResult> Index()
        {
            var list = await _context.NhanViens.ToListAsync();
            return View(list);
        }

        // GET: Admin/NhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nv);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm nhân viên mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Thêm nhân viên thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(nv);
        }

        // GET: Admin/NhanVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null)
                return NotFound();

            return View(nv);
        }

        // POST: Admin/NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, NhanVien nv)
        {
            if (id != nv.MaNhanVien)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nv);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.NhanViens.Any(e => e.MaNhanVien == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy nhân viên để cập nhật.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật nhân viên thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(nv);
        }

        // GET: Admin/NhanVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null)
                return NotFound();

            return View(nv);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv != null)
            {
                _context.NhanViens.Remove(nv);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa nhân viên thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy nhân viên để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

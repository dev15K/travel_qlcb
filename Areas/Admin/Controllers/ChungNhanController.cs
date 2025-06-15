using Microsoft.AspNetCore.Mvc;
using QLCB.Models;
using Microsoft.EntityFrameworkCore;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChungNhanController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChungNhanController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/ChungNhan
        public async Task<IActionResult> Index()
        {
            var dsChungNhan = await _context.ChungNhans.ToListAsync();
            return View(dsChungNhan);
        }

        // GET: Admin/ChungNhan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChungNhan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChungNhan cn)
        {
            if (ModelState.IsValid)
            {
                _context.ChungNhans.Add(cn);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm chứng nhận mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Thêm chứng nhận thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(cn);
        }

        // GET: Admin/ChungNhan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var cn = await _context.ChungNhans.FindAsync(id);
            if (cn == null) return NotFound();

            return View(cn);
        }

        // POST: Admin/ChungNhan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ChungNhan cn)
        {
            if (id != cn.MaChungNhan) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cn);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật chứng nhận thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ChungNhans.Any(e => e.MaChungNhan == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy chứng nhận để cập nhật.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật chứng nhận thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(cn);
        }

        // GET: Admin/ChungNhan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var cn = await _context.ChungNhans.FirstOrDefaultAsync(c => c.MaChungNhan == id);
            if (cn == null) return NotFound();

            return View(cn);
        }

        // POST: Admin/ChungNhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cn = await _context.ChungNhans.FindAsync(id);
            if (cn != null)
            {
                _context.ChungNhans.Remove(cn);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa chứng nhận thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy chứng nhận để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

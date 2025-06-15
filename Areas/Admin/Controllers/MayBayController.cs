using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLCB.Areas.Admin.Models;
using QLCB.Models;
using QLCB.Repositories;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)] // Chỉ cho phép admin truy cập
    public class MayBayController : Controller
    {
        private readonly IMayBayRepository _mayBayRepo;

        public MayBayController(IMayBayRepository mayBayRepo)
        {
            _mayBayRepo = mayBayRepo ?? throw new ArgumentNullException(nameof(mayBayRepo));
        }

        // GET: Admin/MayBay
        public async Task<IActionResult> Index()
        {
            var mayBays = await _mayBayRepo.GetAllAsync();
            return View(mayBays);
        }

        // GET: Admin/MayBay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MayBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MayBay mayBay)
        {
            if (ModelState.IsValid)
            {
                if (await _mayBayRepo.ExistsAsync(mayBay.MaMayBay))
                {
                    ModelState.AddModelError("MaMayBay", "Mã máy bay đã tồn tại.");
                    TempData["ErrorMessage"] = "Thêm máy bay thất bại: Mã máy bay đã tồn tại.";
                    return View(mayBay);
                }

                await _mayBayRepo.AddAsync(mayBay);
                TempData["SuccessMessage"] = "Thêm máy bay mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Thêm máy bay thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(mayBay);
        }

        // GET: Admin/MayBay/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var mayBay = await _mayBayRepo.GetByIdAsync(id);
            if (mayBay == null) return NotFound();

            return View(mayBay);
        }

        // POST: Admin/MayBay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MayBay mayBay)
        {
            if (id != mayBay.MaMayBay) return NotFound();

            if (ModelState.IsValid)
            {
                await _mayBayRepo.UpdateAsync(mayBay);
                TempData["SuccessMessage"] = "Cập nhật máy bay thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật máy bay thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(mayBay);
        }

        // GET: Admin/MayBay/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var mayBay = await _mayBayRepo.GetByIdAsync(id);
            if (mayBay == null) return NotFound();

            return View(mayBay);
        }

        // POST: Admin/MayBay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _mayBayRepo.DeleteAsync(id);
            
            if (!await _mayBayRepo.ExistsAsync(id))
            {
                TempData["SuccessMessage"] = "Xóa máy bay thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa máy bay thất bại.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

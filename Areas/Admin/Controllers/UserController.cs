using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using QLCB.Models.ViewModels;

namespace QLCB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var model = new List<UserWithRoleViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.Add(new UserWithRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? "",
                    CurrentRole = userRoles.FirstOrDefault() ?? "Chưa gán",
                    AllRoles = roles
                });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityUser user, string password)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // Gán vai trò mặc định là "User" cho người dùng mới
                    var roleAssignResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleAssignResult.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Tạo người dùng mới và gán vai trò User thành công!";
                    }
                    else
                    {
                        // Xử lý lỗi nếu không gán được vai trò
                        foreach (var error in roleAssignResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            TempData["ErrorMessage"] = "Tạo người dùng thành công nhưng gán vai trò User thất bại: " + error.Description;
                        }
                    }
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["ErrorMessage"] = "Tạo người dùng thất bại: " + error.Description;
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng để cập nhật.";
                    return NotFound();
                }
                existingUser.Email = user.Email;
                existingUser.UserName = user.Email; // Hoặc giữ nguyên username nếu không muốn thay đổi theo email
                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật người dùng thành công!";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["ErrorMessage"] = "Cập nhật người dùng thất bại: " + error.Description;
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(User); // Lấy người dùng hiện tại

            if (user != null)
            {
                if (user.Id == currentUser.Id)
                {
                    ModelState.AddModelError(string.Empty, "Bạn không thể xóa tài khoản của chính mình.");
                    TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của chính mình.";
                    return View(user); // Trả về view Delete với lỗi
                }
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Xóa người dùng thành công!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        TempData["ErrorMessage"] = "Xóa người dùng thất bại: " + error.Description;
                    }
                    return View(user); // Trả về view Delete với lỗi nếu có lỗi xóa
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng để xóa.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(selectedRole))
            {
                TempData["ErrorMessage"] = "Người dùng hoặc vai trò không hợp lệ.";
                return RedirectToAction("Index");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                {
                    TempData["ErrorMessage"] = "Thay đổi vai trò thất bại: " + error.Description;
                }
                return RedirectToAction("Index");
            }

            var addResult = await _userManager.AddToRoleAsync(user, selectedRole);
            if (addResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Thay đổi vai trò thành công!";
            }
            else
            {
                foreach (var error in addResult.Errors)
                {
                    TempData["ErrorMessage"] = "Thay đổi vai trò thất bại: " + error.Description;
                }
            }

            return RedirectToAction("Index");
        }

        // Thêm các hành động khác như Create, Edit, Delete sau
    }
}

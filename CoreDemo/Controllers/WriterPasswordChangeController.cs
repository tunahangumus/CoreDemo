using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class WriterPasswordChangeController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public WriterPasswordChangeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(PasswordChangeModel passwordChangeModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (passwordChangeModel.currentPassword != null)
                {
                    if (passwordChangeModel.newPassword == passwordChangeModel.confirmNewPassword)
                    {
                        if (passwordChangeModel.newPassword == null)
                        {
                            ModelState.AddModelError("", "Yeni şifre boş olamaz");
                        }
                        else
                        {
                            var result = await _userManager.ChangePasswordAsync(user, passwordChangeModel.currentPassword, passwordChangeModel.newPassword);
                            if (!result.Succeeded)
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError("", item.Description);
                                }
                            }
                            else
                            {
                                await _userManager.UpdateAsync(user);
                                await _userManager.UpdateSecurityStampAsync(user);
                                await _signInManager.SignOutAsync();
                                await _signInManager.SignInAsync(user, true);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Yeni şifre/Yeni şifre tekrar uyuşmuyor");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şu anki şifre boş geçilemez");
                }
            }
            return View();
        }
    }
}

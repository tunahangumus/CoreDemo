using CoreDemo.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
			_userManager = userManager;
        }

		public IActionResult Login(string? returnUrl)
		{
			if (returnUrl != null)
			{
				TempData["ReturnUrl"] = returnUrl;
			}
			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Login(UserSignInViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(viewModel.UserName);
				if (user != null)
				{
					await _signInManager.SignOutAsync();
					var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password,true,true);
					if (result.Succeeded)
					{
						await _userManager.ResetAccessFailedCountAsync(user);
						await _userManager.SetLockoutEndDateAsync(user, null);

						var returnUrl = TempData["ReturnUrl"];
						if (returnUrl != null)
						{
							return RedirectToAction($"Index", "Blog");
						}
						return RedirectToAction($"Index/", "Blog");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Kullanıcı adı ya da şifre hatalı");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Kullanıcı adı ya da şifre hatalı");
				}
			}

			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Blog");
		}
	}
}

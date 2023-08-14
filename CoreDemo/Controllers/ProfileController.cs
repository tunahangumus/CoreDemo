using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await GetUserAsync();
			return View(user);
		}


		[HttpPost]
		public async Task<IActionResult> Index(ProfileEditModel editUser) {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
			{
                user.NameSurname = editUser.nameSurname;
                user.UserName = editUser.userName;
                user.Email = editUser.email;
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);
                return View(editUser);

            }
            return View(editUser);
		}



		public async Task<ProfileEditModel> GetUserAsync()
		{
			var user = User.Identity.Name;
			if (user == null)
			{
				return new ProfileEditModel
				{
					nameSurname = null, email=null, userName=null 
				};
			}


			var x = await _userManager.FindByNameAsync(user);
			return new ProfileEditModel
			{
				nameSurname = x.NameSurname,
				email = x.Email,
				userName = x.UserName
			};
		}
	}
}

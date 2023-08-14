using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CoreDemo.ViewComponents.Blog
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterPanel : ViewComponent
	{
		private readonly UserManager<AppUser> _userManager;

		public WriterPanel(UserManager<AppUser> usermanager)
		{
			_userManager = usermanager;
		}
		public IViewComponentResult Invoke()
		{
			var user = _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.ID = user.Id;
			return View();
		}
	}
}

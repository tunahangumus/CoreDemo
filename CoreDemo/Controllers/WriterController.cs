using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
        [AllowAnonymous]
        public IActionResult Test()
		{
			return View();
		}
        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}
        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

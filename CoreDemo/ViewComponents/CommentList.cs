using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
	public class CommentList: ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var commentvalues = new List<UserComent>() {
				new UserComent
				{
					ID = 1,
					Username = "Ahmet"
				},
				new UserComent
				{
					ID = 2,
					Username = "Mehmet"
				},
				new UserComent
				{
					ID = 3,
					Username = "Veli"
				},
			};

			return View(commentvalues);
		}
	}
}

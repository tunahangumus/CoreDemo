﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class ContactController : Controller
	{
		ContactManager cm = new ContactManager(new EfContactRepository());
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Contact c)
		{
			c.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			c.ContactStatus = true;
			cm.ContactAdd(c);
			return RedirectToAction("Index", "Blog");
		}
	}
}

using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CoreDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        public AdminController( UserManager<AppUser> usermanager)
        {
            _userManager = usermanager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {
            WriterManager wm = new WriterManager(new EfWriterRepository());
            List<Writer> writerList = wm.GetList();
            return View(writerList);
        }

        public async Task<IActionResult> WriterDelete(string deneme)
        {
            WriterManager wm = new WriterManager(new EfWriterRepository());
            wm.Delete(wm.GetByName(deneme));
            var user = await _userManager.FindByNameAsync(deneme);
            await _userManager.RemoveFromRoleAsync(user, "Writer");

            return RedirectToAction("WriterList", "Admin");
        }

        public async Task<IActionResult> WriterAddRole(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            await _userManager.AddToRoleAsync(user, "Writer");
            WriterManager wm = new WriterManager(new EfWriterRepository());
            var writer = wm.GetByName(name);
            writer.WriterStatus = true;
            wm.Update(writer);
            return RedirectToAction("WriterList","Admin");
        }


        public ActionResult OfficialWriterList(string deneme)
        {
            WriterManager wm = new WriterManager(new EfWriterRepository());
            List<Writer> writerList = wm.GetList();
            return View(writerList);
        }
    }
}

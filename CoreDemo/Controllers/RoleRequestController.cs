using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CoreDemoApi.DataAccess;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Concrete;

namespace CoreDemo.Controllers
{
	public class RoleRequestController : Controller
	{
		UserManager<AppUser> _userManager;

        public RoleRequestController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(EmailModel mailToSend)
		{
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            MailMessage newmail = new MailMessage();
			newmail.To.Add("fehmitunahann@gmail.com");
			newmail.From = new MailAddress("fehmitunahannn@gmail.com");
			newmail.Subject = "Yeni bir yazar başvurusu var!";
			newmail.Body = "Sayın yetkili, başvuru yapan yazarın mesajı şu şekilde: <br>" + mailToSend.Content + $"<br>"+ User.Identity.Name + $"<br> { mailToSend.Mail}";
			newmail.IsBodyHtml = true;

			SmtpClient smtp = new SmtpClient();
			smtp.Credentials = new NetworkCredential("fehmitunahannn@gmail.com", "ueikavqsxabztujr");
			smtp.Port = 587;
			smtp.Host = "smtp.gmail.com";
			smtp.EnableSsl = true;

			WriterManager wm = new WriterManager(new EfWriterRepository());
			if (wm.GetByName(user.UserName) != null)
			{
				TempData["Message"] = "Hali hazırda başvurunuz bulunmaktadır.";
				return View();
			}
			else
			{
				wm.WriterAdd(new EntityLayer.Concrete.Writer
				{
					WriterID = user.Id,
					WriterMail = user.Email,
					WriterName = user.UserName,
					WriterStatus = false,
					CreateDate = DateTime.Now,

				});
			}


			try
			{
				smtp.Send(newmail);
				TempData["Message"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
			}
			catch (Exception ex)
			{
				TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
			}

			return View();
		}
	}
}

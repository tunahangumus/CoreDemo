using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class PasswordChangeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public PasswordChangeController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetMail)
		{
			var user = await _userManager.FindByEmailAsync(forgetMail.Mail);
			if (user == null)
			{
				throw new Exception("Böyle bir mail sisteme kayıtlı değil");
			}
			string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
			{
				userID = user.Id,
				token = passwordResetToken
			}, HttpContext.Request.Scheme);


			MailMessage newmail = new MailMessage();
			newmail.To.Add($"{forgetMail.Mail}");
			newmail.From = new MailAddress("fehmitunahann@gmail.com");
			newmail.Body = "Değerli kullanıcımız aşağıdaki lini kullanarak şifre yenileme işleminizi tamamlayabilirsiniz:  <br> " + $"{passwordResetTokenLink}";
			newmail.IsBodyHtml = true;

			SmtpClient smtp = new SmtpClient();
			smtp.Credentials = new NetworkCredential("fehmitunahann@gmail.com", "nvldfxnerwclwbhl");
			smtp.Port = 587;
			smtp.Host = "smtp.gmail.com";
			smtp.EnableSsl = true;
			try
			{
				smtp.Send(newmail);
				TempData["Message1"] = "Mailinize doğrulama linki gönderildi!";
				return View();
			}
			catch (Exception ex)
			{
				TempData["Message1"] = "Maile doğrulama linki gönderilemedi. Hata nedeni:" + ex.Message;
				return View();
			}
		}

		[HttpGet]
		public IActionResult ResetPassword(string userID, string token)
		{
			TempData.Clear();
			TempData["userID"] = userID;
			TempData["token"] = token;
			if (userID == null || token == null)
			{
				throw new Exception("Bu sayfaya erişme yetkiniz yok!");
			}
			ViewBag.userID = userID;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
		{
			var userID = TempData["userID"];
			var token = TempData["token"];
			if (resetPassword.Password != resetPassword.ConfirmPassword)
			{
				throw new Exception("Şifreler uyuşmuyor");
			}
			else
			{
				var user = await _userManager.FindByIdAsync(userID.ToString());
				var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPassword.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login", "Login");

				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("",item.Description);
					}
					return View();
				}
			}
		}
	}
}

using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web;

namespace CoreDemo.Controllers
{

	public class BlogController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        public ICategoryService _categoryService;
		public BlogController(IHttpClientFactory httpClientFactory, UserManager<AppUser> usermanager, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = usermanager;
            _categoryService = categoryService;
        }


		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var _user =  (await GetUserAsync());
            ViewBag.ID = _user.ID;
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var responseMessage = await _httpClient.GetAsync("api/Blog");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString);
            return View(values);
        }




		public async Task<AuthUserViewModel> GetUserAsync()
        {
            var user = User.Identity.Name;
            if(user == null)
            {
                return new AuthUserViewModel
                {
                    ID = 0,
                };
            }


            var x = await _userManager.FindByNameAsync(user);
            return new AuthUserViewModel
            {
                ID = x.Id
            };
        }


		public async Task<IActionResult> BlogReadAll(int id)
        {
			var responseMessage = await GetResponseAsync($"api/Blog/{id}");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString);
            ViewBag.i = id;

            var responseMessage1 = await GetResponseAsync($"api/Blog/" + "LastBlogs/" + $"{id}");
            var jsonString1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString1);
			ViewBag.model = values1;
            return View(values);
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string uri)
        {
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            return await _httpClient.GetAsync(uri);
        }

        [Authorize(Roles = "Admin,Writer")]
        public async Task<IActionResult> BlogListByWriter()
        {
            var p = await GetUserAsync();
			var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var responseMessage = await _httpClient.GetAsync("api/Blog/" + "GetBlogs/" + $"{p.ID}");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString);
            ViewBag.i = p.ID;
            return View(values);
        }

        [Authorize(Roles = "Admin,Writer")]
        [HttpGet]
        public async Task<IActionResult> BlogAdd(int id)
        {
			var _httpClient = _httpClientFactory.CreateClient("BlogApi");
			var responseMessage = await _httpClient.GetAsync($"api/Blog/GetAdd");
			var jsonString = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<SelectListItem>>(jsonString);
            ViewBag.cv = values;
            ViewBag.WriterID = id;
            return View();
        }
        [Authorize(Roles = "Admin,Writer")]
        [HttpPost]
        public async Task<IActionResult> BlogAdd(BlogModel p)
        {
            var _id = p.WriterID;
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var jsonString = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("api/Blog/" + "AddBlog/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BlogListByWriter", new { id = _id });
            }
            var jsonString2 = await responseMessage.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorViewModel>>(jsonString2);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.errorMessage);
            }
            return View(p);
		}

        [Authorize(Roles = "Admin,Writer")]
        [HttpGet]
        public IActionResult DeleteBlog(BlogModel p)
        {
            return View(p);
        }

        [Authorize(Roles = "Admin,Writer")]
        public async Task<IActionResult> ConfirmDeleteBlog(BlogModel p)
        {
            var _id = p.WriterID;
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var responseMessage = await _httpClient.DeleteAsync("api/Blog/" + $"DeleteBlog/{p.BlogID}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BlogListByWriter", new {id = _id});
            }
            return View("BlogListByWriter");
        }




        [Authorize(Roles = "Admin,Writer")]
        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var responseMessage = await _httpClient.GetAsync($"api/Blog/GetEdit/{id}");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<EditModel>(jsonString);
            return View(values);
        }
        [Authorize(Roles = "Admin,Writer")]
        [HttpPost]
        public async Task<IActionResult> EditBlog(EditModel p)
        {
            var _id = p.blog.WriterID;
            var __id = p.blog.BlogID;
            var _blogmodel = await GetBlogAsync(__id);
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var jsonString = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("api/Blog/" + "PutEdit/", content);
            var jsonString2 = await responseMessage.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorViewModel>>(jsonString2);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BlogListByWriter",new {id = _id});
            }
            foreach ( var error in errors)
            {
                ModelState.AddModelError("", error.errorMessage);
            }
            return View(_blogmodel);
        }
        public async Task<EditModel> GetBlogAsync(int id)
        {
            var _httpClient = _httpClientFactory.CreateClient("BlogApi");
            var responseMessage = await _httpClient.GetAsync($"api/Blog/GetBlog/{id}");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<EditModel>(jsonString);
            return values;
        }
    }
}

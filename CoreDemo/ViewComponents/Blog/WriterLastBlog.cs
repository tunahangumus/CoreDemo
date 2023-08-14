using BusinessLayer.Concrete;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace CoreDemo.ViewComponents.Blog
{
	public class WriterLastBlog: ViewComponent
	{
		//HttpClient _httpClient;
		//string baseUrl;

		//public WriterLastBlog(HttpClient httpClient)
		//{
		//	_httpClient = httpClient;
		//	baseUrl = "https://localhost:7071/api/Blog/";
		//}
		//public async Task<IViewComponentResult> Invoke(int id)
		//{
		//	var responseMessage = await _httpClient.GetAsync($"{baseUrl}"+ "LastBlogs/" + $"{id}");
		//	var jsonString = await responseMessage.Content.ReadAsStringAsync();
		//	var values = JsonConvert.DeserializeObject<List<BlogModel>>(jsonString);
		//	return View(values);
		//}
	}
}

using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemoApi.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		BlogManager bm = new BlogManager(new EfBlogRepository());
		ICategoryService _categoryService;

		public BlogController(ICategoryService categoryService)
		{
			_categoryService = categoryService;

        }


		[HttpGet]
		public IActionResult GetAll()
		{
			var values = bm.GetBlogListWithCategory();
			return Ok(values);
		}



        [HttpGet("{id}")]
        public IActionResult ReadAll(int id) 
		{
            var values = bm.GetBlogByID(id);
            return Ok(values);
        }



		[HttpGet("LastBlogs/{id}")]
		public IActionResult LastBlogs(int id) 
		{
			var blog = bm.GetBlogByID(id);
			if (blog.Count == 0) { return BadRequest(); }
			var values = bm.BlogsWithWriterId(blog[0].WriterID);
			return Ok(values);	
		}


		[HttpGet("GetBlogs/{id}")]
		public IActionResult GetBlogs(int id)
		{
			var values = bm.CategoriesWithWriterId(id);
			return Ok(values);
		}



		[HttpGet("GetAdd")]
		public IActionResult AddBlog()
		{
			List<SelectListItem> categoryvalues = (from x in _categoryService.GetList()
												   select new SelectListItem
												   {
													   Text = x.CategoryName,
													   Value = x.CategoryID.ToString()
												   }).ToList();
			return Ok(categoryvalues);
		}

		[HttpPost("AddBlog")]
		public IActionResult AddBlog(Blog p)
		{
			BlogValidator bv = new BlogValidator();
			ValidationResult results = bv.Validate(p);
			if(results.IsValid)
			{
				bm.Add(p);
				return Ok();
			}
			else
			{
                return BadRequest(results.Errors);
            }

		}

		[HttpDelete("DeleteBlog/{id}")]
		public IActionResult DeleteBlog(int id)
		{
			var blogvalue = bm.GetByID(id);
			bm.Delete(blogvalue);
			return Ok();
		}


		[HttpGet("GetEdit/{id}")]
		public IActionResult EditBlog(int id) 
		{
            var blogvalue = bm.GetByID(id);
            List<SelectListItem> categoryvalues = (from x in _categoryService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
			return Ok(new EditModel
            {
                blog = blogvalue,
                _categoryvalues = categoryvalues
            });
        }
		[HttpPost("PutEdit")]
		public IActionResult EditBlog(EditModel p)
		{
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p.blog);

			if (results.Errors.Count() > 0)
			{
				return BadRequest(results.Errors);
			}
            List<SelectListItem> categoryvalues = (from x in _categoryService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            p._categoryvalues = categoryvalues;
            
                bm.Update(p.blog);
                return Ok();
           
			
            
		}

		[HttpGet("GetBlog/{id}")]
		public IActionResult GetBlog(int id)
		{
			List<Blog> p = bm.GetBlogByID(id);
            List<SelectListItem> categoryvalues = (from x in _categoryService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
			EditModel deneme = new EditModel();
			deneme.blog = new Blog();
			deneme.blog = p[0];
			deneme._categoryvalues = categoryvalues;
            return Ok(deneme);
		}
	}
}

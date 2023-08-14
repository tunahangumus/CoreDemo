using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IWriterService _writerservice;

        public AdminController(IWriterService writerservice)
        {
            _writerservice = writerservice;
        }

        [HttpGet("Get")]
        public IActionResult GetWriters()
        {
            WriterManager wm = new WriterManager(new EfWriterRepository());
            List<Writer> writerList = wm.GetList();
            return Ok(writerList);
        }

        [HttpDelete("DeleteBlog/{_name}")]
        public IActionResult DeleteBlog(string _name)
        {
            var blogvalue = _writerservice.GetByName(_name);
            _writerservice.Delete(blogvalue);
            return Ok();
        }
    }
}

using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace CoreDemo.Models
{
    public class BlogAddModel
    {
        public int BlogID { get; set; }

        public string? BlogTitle { get; set; }

        public string? BlogContent { get; set; }

        public string? BlogThumbnailImage { get; set; }

        public IFormFile? BlogImage { get; set; }

        public DateTime BlogCreateDate { get; set; }

        public bool BlogStatus { get; set; }

        public int CategoryID { get; set; }

        public Category? Category { get; set; }

        public int WriterID { get; set; }

        public AppUser? Writer { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class BlogManager : IBlogService
	{
		IBlogDal _blogDal;

		public BlogManager(IBlogDal blogDal)
		{
			_blogDal = blogDal;
		}

		
		public List<Blog> GetBlogListWithCategory()
		{
			return _blogDal.GetListWithCategory();
		}

		public Blog GetByID(int id)
		{
			using var c = new Context();
			return c.Set<Blog>().Find(id);
		}

		public List<Blog> CategoriesWithWriterId(int id)
		{
			return _blogDal.GetListWithCategoryByWriter(id);
		}

		public List<Blog> GetBlogByID(int id) {
			return _blogDal.GetListAll(x => x.BlogID == id);
		}

		public List<Blog> GetList()
		{
			return _blogDal.GetListAll();
		}

		public List<Blog> BlogsWithWriterId(int id)
		{
			var writer = _blogDal.GetWriter(id);
			return writer;
		}

        public void Add(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void Delete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void Update(Blog t)
        {
            _blogDal.Update(t);
        }
    }
}

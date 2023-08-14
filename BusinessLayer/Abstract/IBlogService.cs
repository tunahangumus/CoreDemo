using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IBlogService: IGenericService<Blog>
	{
		public List<Blog> GetBlogListWithCategory();
		public List<Blog> BlogsWithWriterId(int id);
		public List<Blog> CategoriesWithWriterId(int id);
		public List<Blog> GetBlogByID(int id);

    }
}

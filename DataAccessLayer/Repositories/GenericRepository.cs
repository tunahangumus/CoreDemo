using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        Context context = new Context();
        public void Delete(T item)
        {
            context.Remove(item);
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return context.Set<T>().ToList();
        }

        public void Insert(T item)
        {
            context.Add(item);
            context.SaveChanges();  
        }

		public List<T> GetListAll(Expression<Func<T, bool>> filter)
		{
            using var c = new Context();
			return c.Set<T>().Where(filter).ToList();
		}

		public void Update(T item)
        {
            context.Update(item);
            context.SaveChanges();
        }
    }
}

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WriterManager: IWriterService
	{
		IWriterDal _writerdal;

		public WriterManager(IWriterDal writerdal)
		{
			_writerdal = writerdal;
		}

        public void Add(Writer t)
        {
           _writerdal.Insert(t);
        }

        public void Delete(Writer t)
        {
            _writerdal.Delete(t);
        }

        public Writer GetByID(int id)
        {
            return _writerdal.GetById(id);
        }

        public List<Writer> GetList()
        {
            return _writerdal.GetListAll();
        }
        
        public Writer GetByName(string name)
        {
            return _writerdal.GetByName(name);
        }

        public void Update(Writer t)
        {
            _writerdal.Update(t);
        }

        public void WriterAdd(Writer writer)
		{
			_writerdal.Insert(writer);
		}

    }
}

using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer.Abstract
{
	public interface ICommentService
	{
		void CommentAdd(Comment category);
		//void CommentDelete(Comment category);
		//void CommentUpdate(Comment category);
		List<Comment> GetList(int id);
		//Comment GetByID(int id);
	}
}

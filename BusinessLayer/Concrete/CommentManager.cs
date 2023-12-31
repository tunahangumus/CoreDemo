﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CommentManager : ICommentService
	{
		ICommentDal _commentDal;

		public CommentManager(ICommentDal commentDal)
		{
			_commentDal = commentDal;
		}

		public void CommentAdd(Comment category)
		{
			_commentDal.Insert(category);
		}

		/*public void CommentDelete(Comment category)
		{
			throw new NotImplementedException();
		}

		public void CommentUpdate(Comment category)
		{
			throw new NotImplementedException();
		}

		public Comment GetByID(int id)
		{
			throw new NotImplementedException();
		}*/

		public List<Comment> GetList(int id)
		{
			return _commentDal.GetListAll(x => x.BlogID == id);
		}

	}
}

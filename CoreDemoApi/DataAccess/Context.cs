using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CoreDemoApi.DataAccess
{
	public class Context: DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=DESKTOP-5FSH2VU\\SQLEXPRESS;database=CoreBlogDb; integrated security= true;TrustServerCertificate=True;");
		}



		public DbSet<Employee>? Employees { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace CoreDemoApi.DataAccess
{
	public class Employee
	{
		[Key]
		public int ID { get; set; }	
		public string? Name { get; set; }
	}
}

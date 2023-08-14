using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public string? WriterName { get; set; }
        public int WriterID { get; set; }
        public string? WriterMail { get; set; }
        public bool WriterStatus { get; set;}
        public DateTime CreateDate { get; set; }
	}
}

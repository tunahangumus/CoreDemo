using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Models
{
    public class EditModel
    {
        public List<SelectListItem>? _categoryvalues { get; set; }
        public Blog? blog { get; set; }
    }
}

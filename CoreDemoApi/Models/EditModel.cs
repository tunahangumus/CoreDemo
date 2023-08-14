using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemoApi.Models
{
    public class EditModel
    {
        public List<SelectListItem> ?_categoryvalues {  get; set; }
        public Blog ?blog { get; set; }
    }
}

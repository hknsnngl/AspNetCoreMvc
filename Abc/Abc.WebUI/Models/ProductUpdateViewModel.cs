using Abc.Entities;
using System.Collections.Generic;

namespace Abc.WebUI.Models
{
    public class ProductUpdateViewModel
    {
        public List<Category> Categories { get; internal set; }
        public Product Product { get; set; }
    }
}

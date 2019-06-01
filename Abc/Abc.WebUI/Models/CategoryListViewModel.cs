using Abc.Entities;
using System.Collections.Generic;

namespace Abc.WebUI.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; internal set; }
        public int CurrentCategory { get; set; }
    }
}

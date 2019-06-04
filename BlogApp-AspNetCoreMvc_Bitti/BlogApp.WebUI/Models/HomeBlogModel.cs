using BlogApp.Entity;
using System.Collections.Generic;
namespace BlogApp.WebUI.Models
{
    public class HomeBlogModel
    {
        public List<Blog> SliderBlogs { get; set; }
        public List<Blog> HomeBlogs { get; set; }
    }
}

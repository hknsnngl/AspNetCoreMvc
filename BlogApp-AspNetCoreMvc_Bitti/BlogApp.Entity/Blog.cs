using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
namespace BlogApp.Entity
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        [BindNever]                             //Form üzerinde gönderilmemesi için.
        public DateTime Date { get; set; }
        public bool isApproved { get; set; }         
        //1 Blog 1 Kategoriye sahiptir.
        public int CategoryId { get; set; }                 
        public Category Category { get; set; }        
        //Hangileri Anasayfada gösterilsin ?
        public bool isHome { get; set; }
        //Slider da gösterilsin mi?
        public bool isSlider { get; set; }
    }
}

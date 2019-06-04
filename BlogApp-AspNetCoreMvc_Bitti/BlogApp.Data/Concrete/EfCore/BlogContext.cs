using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
namespace BlogApp.Data.Concrete.EfCore
{
    //Entity proje referans edilmeli.(.WebUI da  .Entity ve.Data ekle)
    public class BlogContext:DbContext
    {
        //Bu bloga gelen ConntectionString ile VT oluşacak
        public BlogContext(DbContextOptions<BlogContext> options)   
            :base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

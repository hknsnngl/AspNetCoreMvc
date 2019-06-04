using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Linq;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfBlogRepository : IBlogRepository
    {
        private BlogContext context;
        public EfBlogRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog entity)
        {
            context.Blogs.Add(entity);
            context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var Blog = context.Blogs.FirstOrDefault(c => c.BlogId == blogId);
            if (Blog != null)
            {
                context.Blogs.Remove(Blog);
                context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs;
        }

        public Blog GetById(int BlogId)
        {
            return context.Blogs.FirstOrDefault(c => c.BlogId == BlogId);
        }

        public void SaveBlog(Blog entity)
        {
            if(entity.BlogId == 0)  //Kayıtta tarıh eklemek ıstıyorum
            {
                entity.Date = DateTime.Now;
                context.Blogs.Add(entity);
                
            }
            else                    //Güncellemde tarıh guncellemek ıstemıyorum suan.
            {
                var sonuc = GetById(entity.BlogId);
                if (sonuc != null)
                {
                    sonuc.Title = entity.Title;
                    sonuc.Description = entity.Description;
                    sonuc.Body = entity.Body;
                    sonuc.CategoryId = entity.CategoryId;
                    sonuc.Image = entity.Image;
                    sonuc.isHome = entity.isHome;
                    sonuc.isApproved = entity.isApproved;
                    sonuc.isSlider = entity.isSlider;
                    
                }
            }
            context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
            /* 
                Eski UpdateBlog ??? Burda tarıhı guncelliyor istemesemde.
                context.Entry(entity).State = EntityState.Modified;     //State => Modified,Added,Deleted,...
                context.SaveChanges();

                Neden sıkıntılı çünkü? Edit formunda alanı olmayan Date var.
                Güncelle butonu basınca default bir deger atara ama biz güncellemedik sıkıntı onun için yukardaki yöntem
             */

            var sonuc = GetById(entity.BlogId);
            if (sonuc != null)
            {
                sonuc.Title = entity.Title;
                sonuc.Description = entity.Description;
                sonuc.CategoryId = entity.CategoryId;
                sonuc.Image = entity.Image;
                context.SaveChanges();
            }
        }


    }
}




using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private BlogContext context;
        public EfCategoryRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddCategory(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if(category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void SaveCategory(Category entity)
        {
            if (entity.CategoryId == 0)  
            {
                context.Categories.Add(entity);
            }
            else                   
            {
                var sonuc = GetById(entity.CategoryId);
                if (sonuc != null)
                {
                    sonuc.Name = entity.Name;               
                    
                }
            }
            context.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {                          
            context.Entry(entity).State = EntityState.Modified;     //State => Modified,Added,Deleted,...
            context.SaveChanges();
        }
    }
}

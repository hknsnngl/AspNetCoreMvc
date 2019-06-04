using BlogApp.Entity;
using System.Linq;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        Blog GetById(int blogId);       // Id ile getirme. Detaylandırma
        IQueryable<Blog> GetAll();          // Tümünü getir. Listeleme
        void AddBlog(Blog entity);      //Model ile ekleeme. Ekleme
        void UpdateBlog(Blog entity);   //Model ile güncelleme. Güncelleme
        void DeleteBlog(int blogId);    //Id ile seçip silme. Silme
        //AddOrUpdate için
        void SaveBlog(Blog entity);
    }
}

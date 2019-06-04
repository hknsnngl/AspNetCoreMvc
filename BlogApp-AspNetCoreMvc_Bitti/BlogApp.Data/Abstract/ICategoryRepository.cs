using BlogApp.Entity;
using System.Linq;

namespace BlogApp.Data.Abstract
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);       // Id ile getirme. Detaylandırma
        IQueryable<Category> GetAll();          // Tümünü getir. Listeleme
        void AddCategory(Category entity);      //Model ile ekleeme. Ekleme
        void UpdateCategory(Category entity);   //Model ile güncelleme. Güncelleme
        void DeleteCategory(int categoryId);    //Id ile seçip silme. Silme
        //AddOrUpdate için
        void SaveCategory(Category entity);
    }
}

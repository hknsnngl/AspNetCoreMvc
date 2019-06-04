using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryRepository categoryRepository;
        public CategoryMenuViewComponent(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["id"];//null olursa Values degerine gelmez.Hata olmaması ııcn.
            return View(categoryRepository.GetAll());   //Defualt.cshtml gider başka html'e yonlendırebiliriz de.
        }
    }
}

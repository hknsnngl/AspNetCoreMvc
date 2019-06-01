using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Business.Abstract;
using Abc.Entities;
using Abc.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abc.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(productListViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (ModelState.IsValid) //doğrulama başarılı ise...
            {
                _productService.Add(product);
                TempData.Add("message", "product  was successfully added");
            }
            return RedirectToAction("Add"); //o Action'ın çalışmasını isteriz. DIKKAT!!!
            //return View();  //aksi halde sayfayı yeniden göster...
        }

        [HttpGet]
        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Product product)
        {

            if (ModelState.IsValid) //doğrulama başarılı ise...
            {
                _productService.Update(product);
                TempData.Add("message", "product  was successfully updated");
            }
            return RedirectToAction("Update"); //o Action'ın çalışmasını isteriz. DIKKAT!!!
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            TempData.Add("message", "product  was successfully deleted");
            return RedirectToAction("Index");   //Sildikten sonra Index Actiona git...
        }
    }
}
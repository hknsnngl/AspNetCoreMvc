using Abc.Business.Abstract;
using Abc.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Abc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page=1, int category=0)
        {
            int pageSize = 10;
            var products = _productService.GetByCategory(category);
            ProductListViewModel model = new ProductListViewModel()
            {
                //sol tarftakiler ProductListViewModel deki propertyiler.
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),                
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page
            };
            return View(model);
        }

        //public string Session()
        //{
        //    HttpContext.Session.SetString("city", "ordu"); //string olarak tutma. key/value
        //    HttpContext.Session.SetInt32("Age", 26);       //int olarak tutma.

        //    HttpContext.Session.GetString("city");         //session bilgisini okuma.
        //    HttpContext.Session.GetInt32("age");    
        //}

    }
}
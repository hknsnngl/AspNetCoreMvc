using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(categoryRepository.GetAll());
        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return View(new Category());    //onemli
            }
            else
            {
                return View(categoryRepository.GetById((int)id));
            }
        }
        [HttpPost]
        public IActionResult AddOrUpdate(Category entity)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.SaveCategory(entity);
                TempData["msj"] = $"{entity.Name} kaydedildi";
                return RedirectToAction("List");
            }
            //problem varsa, modeli tekrar aynı sayfaya yollar.
            return View(entity);
        }
        



    }
}

//AddOrUpdate den önce

/*
         [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category entity)
    {
        if (ModelState.IsValid)
        {
            categoryRepository.AddCategory(entity);
            return RedirectToAction("List");
        }
        return View(entity);
    }

    */
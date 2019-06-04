using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        IBlogRepository blogRepository;
        ICategoryRepository categoryRepository;                                                             //BlogControllerda kullanıcagım ıcın tanımladım.
        public BlogController(IBlogRepository _blogRepository,ICategoryRepository _categoryRepository)      //C -> I => EF:I -> Context 
        {
            blogRepository = _blogRepository;
            categoryRepository = _categoryRepository;                               
        }
        public IActionResult Index(int? id, string q)
        {   //gelen CategoryId
            var sorgu = blogRepository.GetAll()
                    .Where(i => i.isApproved);

            if (id != null)
            {
                sorgu = sorgu.Where(i => i.CategoryId == id);
            }

            if (!string.IsNullOrEmpty(q))
            {
                //sorgu = sorgu.Where(i => i.Title.Contains(q) || i.Description.Contains(q) || i.Body.Contains(q));
                sorgu = sorgu.Where(i => EF.Functions.Like(i.Title, "%" + q + "%")|| EF.Functions.Like(i.Description, "%" + q + "%")|| EF.Functions.Like(i.Body, "%" + q + "%"));
            }

             return View(sorgu.OrderByDescending(i => i.Date));              
        }

        public IActionResult List()
        {
            return View(blogRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(blogRepository.GetById(id));
        }

        //CREATE
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
                return View(new Blog());         
        }

        [HttpPost]
        public IActionResult Create(Blog entity)
        {
            if (ModelState.IsValid)
            {
                blogRepository.SaveBlog(entity);
                TempData["msj"] = $"{entity.Title} kayıt edildi!!!";     //VIEW'A MESAJ YOLLAMA,VERILER GUNCELENDIKTEN SONRA
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);

        }

        //EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(blogRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)   //IFormFile resim yüklemek için. Birden fazla yüklencek ise IENumerable<IFormFile> ve async Task olamsı gerek
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    //Resim için
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); //Kayıt işlemi yapıldığı an.
                    }

                    entity.Image = file.FileName;       //resim ismini göndermek.
                                                        //Resim Bitti.
                }

                blogRepository.SaveBlog(entity);
                TempData["msj"] = $"{entity.Title} kayıt edildi!!!";     //VIEW'A MESAJ YOLLAMA,VERILER GUNCELENDIKTEN SONRA
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);

        }


























        //ADDORUPDATE
        [HttpGet]
        public IActionResult AddOrUpdate(int? id)    //id nullable cunku kayıtta id gondermıcem ama guncellerken id göndercem.
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");

            if (id == null)//Yeni kayıt
            {
                return View(new Blog());//BOŞ NESNE GONDERILDIGINDE AddOrUpdate View'da id degerı tutuluyordu ona 0 yollarız.
            }
            else            //Güncelleme
            {
                return View(blogRepository.GetById((int)id));
            }

            
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Blog entity)
        {
            if (ModelState.IsValid)
            {
                blogRepository.SaveBlog(entity);
                TempData["msj"] = $"{entity.Title} kayıt edildi!!!";     //VIEW'A MESAJ YOLLAMA,VERILER GUNCELENDIKTEN SONRA
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);
           
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(blogRepository.GetById(id));
        }
        [HttpPost,ActionName("Delete")]     //önemli
        public IActionResult DeleteConfirmed(int id)
        {
            blogRepository.DeleteBlog(id);
            TempData["msj"] = $"{id} id'li kayıt silindi!!!";     //VIEW'A MESAJ YOLLAMA,VERILER GUNCELENDIKTEN SONRA
            return RedirectToAction("List");
        }



    }
}

//AddOrUpdate'den Önce
/* 
            [HttpGet]               
        public IActionResult Create()                  //CREATE FORM OLUSTURMA
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog entity)       //FORMDAN GELEN VERİ İLE VT'A CREATE ISLEMI.
        {
            entity.Date = DateTime.Now;                //BINDNEVER OLD. BIZ TANIMLADIK, VT KAYDEDERKEN HATA OLMAMASI ICIN.
            if(ModelState.IsValid)                     //MODEL DEKİ VALIDATİONLAR SAGLANMIŞ İSE
            {
                blogRepository.AddBlog(entity);
                return RedirectToAction("List");       //BU METOD LİST METODUNA GIDER DAHA DA BURAYA GELMEZ.
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);
        }

                                                        //O ID LI DEGERININ FORM OLUSUTURUP VERİLERİ İLE BIRLIKTE FORMDA GOSTERME
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(blogRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Blog entity)
        {
            if (ModelState.IsValid)
            {
                blogRepository.UpdateBlog(entity);
                TempData["msj"] = $"{entity.Title} güncellendi!!!";     //VIEW'A MESAJ YOLLAMA,VERILER GUNCELENDIKTEN SONRA
                return RedirectToAction("List");                        //BU SATIRA GELİRSE ACTION METOD LİST METODUNU ÇALISTIRIR.
            }
            ViewBag.Categories = new SelectList(categoryRepository.GetAll(), "CategoryId", "Name");
            return View(entity);
        } 
     

 
*/

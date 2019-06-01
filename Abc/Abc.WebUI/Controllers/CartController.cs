using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Business.Abstract;
using Abc.Entities;
using Abc.WebUI.Models;
using Abc.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abc.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartSessionService _cartSessionService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
        }
        public ActionResult AddToCart(int productId)
        {
            var productToBeAdded = _productService.GetById(productId);
            var cart = _cartSessionService.GetCart();
            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", String.Format("Your product, {0}, was successfully added to the cart!", productToBeAdded.ProductName));
            return RedirectToAction("Index", "Product");
        }
        
        public ActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            CartListViewModel cartListViewModel = new CartListViewModel()
            {
                Cart = cart
            };
            return View(cartListViewModel);
        }

        public ActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", String.Format("Your product, was successfully removed from the cart!"));

            return RedirectToAction("List");
        }

        [HttpGet]   //Form gönderme
        public ActionResult Complete()
        {
            var shippingDetailsViewModel = new ShippingDetailsViewModel//()
            {
                ShippingDetails = new ShippingDetails()
            };
            return View(shippingDetailsViewModel);
        }

        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            if(!ModelState.IsValid) //girilen model geçerli değilse
            {
                return View();      //hata mesajlarını gösterir.
            }
            TempData.Add("message", string.Format("Thank you {0},you ordeer is in process", shippingDetails.FirstName));
            return View();
        }
    }
}
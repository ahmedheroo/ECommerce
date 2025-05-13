using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using BaytonicECommerce.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using BaytonicECommerce.Helper;

namespace BaytonicECommerce.Controllers
{
    //session link  
    //https://learningprogramming.net/net/asp-net-core-mvc/build-shopping-cart-with-session-in-asp-net-core-mvc/
    public class ClientCartWithSession : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;
        IStringLocalizer<Resource> T;


        public ClientCartWithSession(IProductRepository _productRepository, ICartRepository _cartRepository)
        {
            productRepository = _productRepository;
            cartRepository = _cartRepository;
        }

        [HttpGet]
        public IActionResult GetUserCarts()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        public IActionResult Buy(long id)
        {
          
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product =productRepository.GetById(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart { Product = productRepository.GetById(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(long id)
        {
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(long id)
        {
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
      
    }
}
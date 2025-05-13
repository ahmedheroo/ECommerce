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
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
 
    public class ClientCartController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;
        private readonly IGovernmentShippingRepository governmentShippingRepository;
        private readonly ICouponRepository couponRepository ;

        IStringLocalizer<Resource> T;


        public ClientCartController(IProductRepository _productRepository, ICartRepository _cartRepository, IGovernmentShippingRepository _governmentShippingRepository, ICouponRepository _couponRepository)
        {
            productRepository = _productRepository;
            cartRepository = _cartRepository;
            governmentShippingRepository = _governmentShippingRepository;
            couponRepository = _couponRepository;
        }

        [HttpGet]
        [Route("Cart")]
        public IActionResult GetUserCarts()
        {
            string UserId = "1";
            IEnumerable<Cart> carts = cartRepository.GetAllCartUsingUserIdIncludingProducts(UserId);
            return View(carts);
        }
        [Authorize]
        [Route("CheckOut")]
        public IActionResult GetUserCartWithShips()
        {

        
            Cart_ShipsVM cartvm= new Cart_ShipsVM();
       

            cartvm.Governments = governmentShippingRepository.GetAll().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            cartvm.coupons = couponRepository.GetAll();

            return View(cartvm);
        }

        public IActionResult GetGovShippingPrice(long govId) 
        {
            decimal shippingPrice = governmentShippingRepository.GetById(govId).ShippingPrice.Value;
            return Json(shippingPrice);
        }
        [HttpGet]
        //return partial view from cart table
        public IActionResult GetUserCartDetails()
        {
            string UserId = "1";

            IEnumerable<Cart> carts = cartRepository.GetAllCartUsingUserIdIncludingProducts(UserId);

            return PartialView("_GetUserCartDetails", carts);
        }
        [HttpPost]
        public void AddCartItem( long productId)
        {
            string userId = "1";
            //Check if the product added before  to user cart or not 
            Cart alreadyFoundCart = cartRepository.GetCartUsingUserIdAndProductIdIncludingProduct(userId, productId);
            if (alreadyFoundCart != null)
            {
                NotifyToastr("success", "ThisProductIsAlreadyInYourCart");

            }
            else
            {
                Product product = productRepository.GetById(productId);
               
                alreadyFoundCart = new Cart()
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1,
                    ImgUrl = product.FirstImgUrl
                   
                };
                if (product.Offer>0)
                {
                    alreadyFoundCart.TotalPrice = product.Offer;
                }
                else
                    alreadyFoundCart.TotalPrice = product.Price;
                cartRepository.Insert(alreadyFoundCart);
            //    NotifyToastr(T["success"], T["ProductAddedToYourCartSuccessfully"]);

            }
        }
        [HttpPost]
        public IActionResult DeleteCartItem(long cartId)
        {
            try {
               
                cartRepository.Delete(cartId);
                return RedirectToAction("GetUserCartDetails");

                // NotifyToastr(T["success"], T["ProductRemovedFromYourCartSuccessfully"]);

            }
            catch
            {
                return Json(0);

                //   NotifyToastr(T["error"], T["Can'tRemoveProductFromYourCart"]);

            }
        }
        public IActionResult UpdateCartQuantity(long cartId,int NewQuantity)
        {
            try
            {
                Cart cart = cartRepository.GetById(cartId);
                cart.Quantity = NewQuantity;
                Product product = productRepository.GetById(cart.ProductId);
                if (product.Offer>0)
                {
                    cart.TotalPrice = NewQuantity * (product.Offer);
                }
                else
                cart.TotalPrice = NewQuantity * (product.Price);
                cartRepository.Update(cart);
                // NotifyToastr(T["success"], T["ProductRemovedFromYourCartSuccessfully"]);
               return RedirectToAction("GetUserCartDetails");

            }
            catch
            {
                return Json(0);

            }
        }
    }
}
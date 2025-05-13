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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
    [Authorize]
    public class ClientWishListController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly IWishListRepository wishListRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        IStringLocalizer<Resource> T;


        public ClientWishListController(IProductRepository _productRepository, IWishListRepository _wishListRepository, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            productRepository = _productRepository;
            wishListRepository = _wishListRepository;
            userManager = _userManager;
            signInManager = _signInManager;

        }

        [HttpGet]
        public IActionResult GetUserWishList()
        {
            string UserId = userManager.GetUserId(User);
            IEnumerable<WishList> wishLists = wishListRepository.GetUserWishListsIncludeProduct(UserId);
            return View(wishLists);
        }
        [HttpGet]
        //return partial view from WishList table
        public IActionResult GetUserWishListDetails()
        {
            string UserId = userManager.GetUserId(User);
            IEnumerable<WishList> wishLists = wishListRepository.GetUserWishListsIncludeProduct(UserId);
            return PartialView("_GetUserWishListDetails", wishLists);

        }
        [HttpPost]
        public void AddWishListItem( long productId)
        {
            string userId = userManager.GetUserId(User);
            //Check if the product added before  to user cart or not 
            WishList alreadyFoundWishList = wishListRepository.GetWishListUsingUserIdAndProductId(userId, productId);
            if (alreadyFoundWishList != null)
            {
                //NotifyToastr(T["success"], T["ThisProductIsAlreadyInYourWishList"]);

            }
            else
            {
                Product product = productRepository.GetById(productId);
                alreadyFoundWishList = new WishList()
                {
                    UserId = userId,
                    ProductId = productId,
                    ImgUrl = product.FirstImgUrl
                };
                wishListRepository.Insert(alreadyFoundWishList);
                //NotifyToastr(T["success"], T["ProductAddedToYourWishListSuccessfully"]);

            }
        }
        [HttpPost]
        public IActionResult DeleteWishListItem(long wishListId)
        {
            try {
                wishListRepository.Delete(wishListId);
                return RedirectToAction("GetUserWishListDetails");

                //NotifyToastr(T["success"], T["ProductRemovedFromYourWishListSuccessfully"]);
                //  NotifyToastr("success", "ProductRemovedFromYourWishListSuccessfully");

            }
            catch (Exception ex)
            {
                return Json(0);
                //NotifyToastr(T["error"], T["Can'tRemoveProductFromYourWishList"]);

            }
        }

    }
}
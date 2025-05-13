using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using BaytonicECommerce.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Security.Claims;
using System.Threading.Tasks;

namespace BaytonicECommerce.Controllers
{
 
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        //private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductDescriptionRepository productDescriptionRepository;
        private readonly IMessageRepository messageRepository;
        private readonly INewsLetterRepository newsLetterRepository;
        private readonly UserManager<IdentityUser> userManager; 
        private readonly RoleManager<IdentityRole> roleManager;
        string userId;
      
        public HomeController(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IProductImageRepository _productImageRepository,
            IProductDescriptionRepository _productDescriptionRepository,IMessageRepository _messageRepository, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager
            ,INewsLetterRepository _newsLetterRepository)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
            productImageRepository = _productImageRepository;
            productDescriptionRepository = _productDescriptionRepository;
            messageRepository = _messageRepository;
            newsLetterRepository = _newsLetterRepository;
            userManager = _userManager;
            roleManager = _roleManager;
        }
       

        public IActionResult Index()
        {
           
                HomePageVM homePageVM = new HomePageVM()
                {
                    TopSellerProducts = productRepository.GetTopSellerProducts(),
                    ProductsWithOffers = productRepository.GetTopTenProductWithOffer(),
                    TopCategories = categoryRepository.GetTopSixCategories(),
                    ShowInHomeCategories = categoryRepository.GetShowInHomeCategories()
                };
            return View(homePageVM);
        }
        //[HttpGet]
        //public async Task<IActionResult> CreateRoles() 
        //{
        //  var  roleResult = await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //  var  roleResult1 = await roleManager.CreateAsync(new IdentityRole("Admin"));
        //  var  roleResult2 = await roleManager.CreateAsync(new IdentityRole("User"));
        //    return Json(0);
        //}
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {

            Response.Cookies.Append(
                 CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUS()
        {
            return View();
        }
        public IActionResult ContactUS()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.Date = DateTime.Now;
            messageRepository.Insert(message);
            NotifyToastr("success", "Message Sent Successfully");
            return RedirectToAction("ContactUS");
        }
        [HttpGet]
        public IActionResult Policy()
        {
            return PartialView();
        }
        public IActionResult CreateNewsLetter(string email)
        {
            if (newsLetterRepository.GetByEmail(email) == null)
            {
                NewsLetter newsLetter = new NewsLetter()
                {
                    IsActive = false,
                    ClientMail = email
                };
                newsLetterRepository.Insert(newsLetter);
                //NotifyToastr("success", "Subscribed Successfully");
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        [HttpPost]
        public IActionResult KeepSessionAlive()
        {

            return Json(0);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

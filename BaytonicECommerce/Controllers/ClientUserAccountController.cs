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
//using BaytonicECommerce.Authentication;
using System.Net.Http;
using BaytonicECommerce.Helper;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BaytonicECommerce.Controllers
{
    public class ClientUserAccountController : BaseController
    {
        Helpers helpers = new Helpers();

        public IActionResult index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }
        
        //[HttpPost]
        //public IActionResult Login(LoginModel loginModel)
        //{
        //    HttpClient client = helpers.Initiate();
        //    var postTask = client.PostAsJsonAsync<LoginModel>("api/Authenticate/Login", loginModel);
        //    postTask.Wait();
        //    //var userName = HttpContext.User.Identity.Name.ToString();
        //    //var user = userManager.FindByNameAsync(userName);
        //    return Json(postTask.Result);

        //}
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();

        //}
       // [HttpPost]
        //public IActionResult Register(RegisterModel registerModel)
        //{
        //    HttpClient client = helpers.Initiate();
        //    var postTask = client.PostAsJsonAsync<RegisterModel>("api/Authenticate/Register", registerModel);
        //    postTask.Wait();
        //    return Json(0);

        //}
    }
}
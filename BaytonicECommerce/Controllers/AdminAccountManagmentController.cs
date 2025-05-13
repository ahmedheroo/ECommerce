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
using System.Net.Http;

using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminAccountManagmentController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAccountManagementRepository accountRepository;
        public AdminAccountManagmentController(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, IAccountManagementRepository _accountRepository)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            accountRepository = _accountRepository;
        }
        [HttpGet]
        public IActionResult AdminsIndex()
        {
            IEnumerable<AspNetUserRole> Admins = accountRepository.GetAllAdmins();
            return View(Admins);
        }
        [HttpGet]
        public IActionResult UsersIndex()
        {
            IEnumerable<AspNetUserRole> Admins = accountRepository.GetAllUsers();
            return View(Admins);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            User_RoleVM model = new User_RoleVM()
            {
                RoleList = accountRepository.GetAllRoles().Where(x=>x.Name!="SuperAdmin").ToList()
            };
                return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User_RoleVM model)
        {
            var user = new IdentityUser
            {
                UserName = model.User.UserName,
                Email = model.User.Email,
                PhoneNumber = model.User.PhoneNumber
            };
            string roleName = "";
             IdentityResult result=new IdentityResult() ;
            if (model.User.PasswordHash!=null)
            {
                 result = await userManager.CreateAsync(user, model.User.PasswordHash);
            }
            if (result.Succeeded)
            {
                roleName = accountRepository.GetRoleNameUsingRoleId(model.SelectedRoleId);
                var RoleResult = await userManager.AddToRoleAsync(user, roleName);
            }
            else
            {
                ////ErrorMessage
            }
            if (roleName=="User")
            {
                return RedirectToAction("UsersIndex");
            }
            else
            {
                return RedirectToAction("AdminsIndex");

            }
        }
            [HttpGet]
            public IActionResult Edit(string userId)
            {
                AspNetUser user = accountRepository.GetUserById(userId);
            User_RoleVM model = new User_RoleVM()
            {
                RoleList = accountRepository.GetAllRoles().ToList(),
                User = user,
                SelectedRoleId = accountRepository.GetUserRoleId(userId)
            };
            return View(model);
            }
            public async Task<IActionResult> DeleteUser(string userId)
            {
                var user = await userManager.FindByIdAsync(userId);
                var result = await userManager.DeleteAsync(user);
                return View(user);
            }

            [HttpGet]
            public IActionResult Login()
            {

                return View();

            }

        }
    }
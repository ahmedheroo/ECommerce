using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.ViewModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminCategoryController : BaseController
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<AdminCategoryController> _logger;

        public AdminCategoryController(ICategoryRepository _categoryRepository, ILogger<AdminCategoryController> logger)
        {
             categoryRepository = _categoryRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Categories = categoryRepository.GetAll();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Category_ParentVM category_ParentVM = new Category_ParentVM();
            category_ParentVM.Categories= categoryRepository.GetAll();
            return View(category_ParentVM);
        }

        [HttpPost]
        public IActionResult Create(Category_ParentVM category_Parent)
        {
            Category category = new Category();
            {
                category.NameE= category_Parent.category.NameE;
                category.NameA = category_Parent.category.NameA;
                category.ParentId = category_Parent.category.ParentId;
                category.IsActive = category_Parent.category.IsActive;
                category.ShowInHomePage = category_Parent.category.ShowInHomePage;

            }
            NotifyToastr("success", "Data saved successfully");
            categoryRepository.Insert(category);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            Category_ParentVM category_ParentVM = new Category_ParentVM();
            category_ParentVM.Categories = categoryRepository.GetAll();
            category_ParentVM.category = categoryRepository.GetById(id);
            return View(category_ParentVM);
        }
        [HttpPost]
        public IActionResult Edit(Category_ParentVM category_ParentVM)
        {
            category_ParentVM.Categories = categoryRepository.GetAll();
          
            Category category = categoryRepository.GetById(category_ParentVM.Categories.FirstOrDefault().Id);
            if (category!=null)
            {
                category.NameE = category_ParentVM.category.NameE;
                category.NameA = category_ParentVM.category.NameA;
                category.ParentId = category_ParentVM.category.ParentId;
                category.IsActive = category_ParentVM.category.IsActive;
                category.ShowInHomePage = category_ParentVM.category.ShowInHomePage;
            }
            NotifyToastr("success", "Data edited successfully");
            return View(category_ParentVM);
        }
        public IActionResult DeleteToRecycle(long id)
        {
            categoryRepository.DeleteToRecycle(id);

            return RedirectToAction("Index");

        }
        public IActionResult Delete(long id)
        {
            categoryRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

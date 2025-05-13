using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using BaytonicECommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminGovernmentShippingController : BaseController
    {

        private IGovernmentShippingRepository governmentShippingRepository;
        public AdminGovernmentShippingController(IGovernmentShippingRepository _governmentShippingRepository)
        {
            governmentShippingRepository=_governmentShippingRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<GovernmentShipping> AllGovs = governmentShippingRepository.GetAll();
            return View(AllGovs);
        }
        [HttpGet]
        public IActionResult Create()
        {
      
            return View();
        }
        [HttpPost]
        public IActionResult Create(GovernmentShipping model)
        {
          
            governmentShippingRepository.Insert(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            GovernmentShipping gov = new GovernmentShipping();
            gov = governmentShippingRepository.GetById(id);
            return View(gov);
        }
        [HttpPost]
        public IActionResult Edit(GovernmentShipping model)
        {
          
            governmentShippingRepository.Update(model);
            return RedirectToAction("Index");
        }
      
        public IActionResult Delete(long id)
        {
            governmentShippingRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

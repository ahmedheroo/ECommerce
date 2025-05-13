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
    public class AdminCouponController : BaseController
    {
        private readonly ICouponRepository couponRepository;
        private readonly ILogger<AdminCouponController> _logger;

        public AdminCouponController(ICouponRepository _couponRepository, ILogger<AdminCouponController> logger)
        {
             couponRepository = _couponRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Coupon> coupons = couponRepository.GetAll();
            return View(coupons);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coupon model)
        {
           
            NotifyToastr("success", "Data saved successfully");
            couponRepository.Insert(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
          Coupon model = couponRepository.GetById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Coupon model)
        {
          couponRepository.Update(model);
            NotifyToastr("success", "Data Updated successfully");
            return RedirectToAction("Index");
        }
      
        public IActionResult Delete(long id)
        {
            couponRepository.Delete(id);
            NotifyToastr("success", "Data Deleted successfully");

            return RedirectToAction("Index");
        }
    }
}

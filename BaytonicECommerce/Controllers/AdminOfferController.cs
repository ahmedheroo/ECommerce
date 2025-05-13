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
    public class AdminOfferController : BaseController
    {

        private IProductRepository productRepository;
        public AdminOfferController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> OfferProducts = productRepository.GetProductWithOffer();
            return View(OfferProducts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product_OfferVM product_OfferVM = new Product_OfferVM();
            product_OfferVM.Products = productRepository.GetAll();
            return View(product_OfferVM);
        }
        [HttpPost]
        public IActionResult Create(Product_OfferVM product_OfferVM)
        {
            Product OldProduct = productRepository.GetById(product_OfferVM.product.Id);
            OldProduct.Offer = product_OfferVM.product.Offer;
            productRepository.Update(OldProduct);
            return RedirectToAction("Index");
        }
        //Get Product Price as a json result to be shown in offers using ajax
        public IActionResult GetProductPrice(long id)
        {
            Product Product = productRepository.GetById(id);
            decimal price = -1;
            if (Product!=null)
            {
                price = Product.Price;
            }
            return Json(price);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            Product product = new Product();
            product = productRepository.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            Product OldProduct = productRepository.GetById(product.Id);
            OldProduct.Offer = product.Offer;
            productRepository.Update(OldProduct);
            return RedirectToAction("Index");
        }
      
        public IActionResult Delete(long id)
        {
            Product OldProduct = productRepository.GetById(id);
            OldProduct.Offer = 0;
            productRepository.Update(OldProduct);
            return RedirectToAction("Index");
        }
    }
}

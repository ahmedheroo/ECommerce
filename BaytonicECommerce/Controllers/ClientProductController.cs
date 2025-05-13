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

namespace BaytonicECommerce.Controllers
{
    public class ClientProductController : BaseController
    {
        private readonly IProductRepository productRepository;
       
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductDescriptionRepository productDescriptionRepository;

        public ClientProductController(IProductRepository _productRepository, IProductImageRepository _productImageRepository, IProductDescriptionRepository _productDescriptionRepository)
        {
            productRepository = _productRepository;
            productImageRepository = _productImageRepository;
            productDescriptionRepository = _productDescriptionRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<VwproductWithCategoryName> vwproductWithCategoryNames = productRepository.GetAllProductsByCategoryName();
            return View(vwproductWithCategoryNames);
        }
        [HttpGet]
        [Route("/products/")]
        public IActionResult GetProductsByCategoryID(long id)
        {
            IEnumerable<Product> products = productRepository.GetAllByCategory(id);
            return View(products);
        }
        [Route("/search/")]
        public IActionResult GetProductsByTags(string tag)
        {
            IEnumerable<Product> products = productRepository.GetProductByTag(tag);
            return View(products);
        }
        [Route("/Offers/")]
        public IActionResult GetProductsByOffers(string tag)
        {
            IEnumerable<Product> products = productRepository.GetProductWithOffer();
            return View(products);
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            IEnumerable<Product> products = productRepository.GetAll();
            return View(products);
        }
        [HttpGet]
        public IActionResult GetProductsWithOffer()
        {
            IEnumerable<Product> products = productRepository.GetProductWithOffer();
            return View(products);
        }
        [HttpGet]
        public IActionResult GetProductsTopSeller()
        {
            IEnumerable<Product> products = productRepository.GetTopSellerProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult GetProductByIDQuickView(long id)
        {
            ProductDetailsVM productDetailsVM = new ProductDetailsVM()
            {
                product = productRepository.GetById(id),
                productDescriptions = productDescriptionRepository.GetProductDescriptions(id),
                ProductImages = productImageRepository.GetProductImgsByProductID(id)
            };
            return PartialView(productDetailsVM);
        }
        [HttpGet]
        [Route("/product-details/")]
        public IActionResult GetProductDetails(long id)
        {
            Product product = productRepository.GetById(id);
            ProductDetailsVM productDetailsVM = new ProductDetailsVM()
            {
                product = product,
                productDescriptions = productDescriptionRepository.GetProductDescriptions(id),
                ProductImages = productImageRepository.GetProductImgsByProductID(id),
                relatedProducts = productRepository.GetTopTenProductsOfCategory(product.CategoryId)
            };
            return View(productDetailsVM);
        }
    }
}

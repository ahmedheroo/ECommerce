//using BaytonicECommerce.ViewModels;
//using BaytonicECommerce.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BaytonicECommerce.Repository;

//namespace BaytonicECommerce.API
//{

//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IProductRepository productRepository;

//        public ProductsController(IProductRepository _productRepository)
//        {
//            productRepository = _productRepository;

//        }
//        [HttpGet]
//        [Route("api/[controller]")]
//        public IActionResult GetProducts()
//        {
//            List<ProductImgVM1> productImgVMs = new List<ProductImgVM1>();
//            try
//            {
//                List<Product> products = context.Products.Include(x => x.Category).Where(x => x.Isactive == true).ToList();
//                foreach (var item in products)
//                {
//                    ProductImgVM1 productImgVM = new ProductImgVM1();
//                    productImgVM.product = item;
//                    productImgVM.ProductImages = context.ProductImages.Where(x => x.ProductId == item.Id).ToList();
//                    productImgVMs.Add(productImgVM);
//                }
//                return Ok(productImgVMs);
//            }

//            catch
//            {
//                return BadRequest();
//            }

//        }
//        [HttpGet]
//        [Route("api/[controller]/GetProductByCategory/{id}")]
//        public IActionResult GetProducts(long id)
//        {
//            try
//            {

//                List<ProductImgVM> productImgVMs = new List<ProductImgVM>();

//                List<Product> products = context.Products.Include(x => x.Category).Where(x => x.CategoryId == id && x.Isactive == true).ToList();
//                foreach (var item in products)
//                {
//                    ProductImgVM productImgVM = new ProductImgVM();
//                    productImgVM.product = item;
//                    productImgVM.ProductImages = context.ProductImages.Where(x => x.ProductId == item.Id).ToList();
//                    productImgVMs.Add(productImgVM);
//                }

//                return Ok(productImgVMs);
//            }
//            catch
//            {
//                return BadRequest();
//            }

//        }
//        [HttpGet]
//        [Route("api/[controller]/{id}")]
//        public IActionResult GetProduct(long id)
//        {
//            ProductImgVM productImgVM = new ProductImgVM();
//            try
//            {
//                productImgVM.product = context.Products.Where(x => x.Id == id).Include(x => x.Category).FirstOrDefault();
//                productImgVM.ProductImages = context.ProductImages.Where(x => x.ProductId == id).ToList();
//                return Ok(productImgVM);
//            }
//            catch
//            {
//                return BadRequest();
//            }
//        }
//    }
//}

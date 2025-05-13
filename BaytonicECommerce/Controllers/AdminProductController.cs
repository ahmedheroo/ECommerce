using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using BaytonicECommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,SuperAdmin" )]

    public class AdminProductController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductDescriptionRepository productDescriptionRepository;

        public AdminProductController(IProductRepository _productRepository, IWebHostEnvironment _webHostEnvironment, ICategoryRepository _categoryRepository, IProductImageRepository _productImageRepository, IProductDescriptionRepository _productDescriptionRepository)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
            webHostEnvironment = _webHostEnvironment;
            productImageRepository = _productImageRepository;
            productDescriptionRepository = _productDescriptionRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<VwproductWithCategoryName> vwproductWithCategoryNames = productRepository.GetAllProductsByCategoryName();
            return View(vwproductWithCategoryNames);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product_Descriptions_CategoriesVM product_Categories = new Product_Descriptions_CategoriesVM()
            {
                Categories = categoryRepository.GetAll()
            };
            return View(product_Categories);
        }
        [HttpPost]
        public IActionResult Create(Product_Descriptions_CategoriesVM product_Descriptions_CategoriesVM)
        {
            Product p = null;
            if (ModelState.IsValid)
            {
                Product product = new Product()

                {
                    NameE = product_Descriptions_CategoriesVM.product.NameE,
                    NameA = product_Descriptions_CategoriesVM.product.NameA,
                    Price = product_Descriptions_CategoriesVM.product.Price,
                    Amount = product_Descriptions_CategoriesVM.product.Amount,
                    IsActive = product_Descriptions_CategoriesVM.product.IsActive,
                    CategoryId = product_Descriptions_CategoriesVM.product.CategoryId,
                    HintE = product_Descriptions_CategoriesVM.product.HintE,
                    HintA = product_Descriptions_CategoriesVM.product.HintA,
                    TagE=product_Descriptions_CategoriesVM.product.TagE,
                    TagA = product_Descriptions_CategoriesVM.product.TagA,
                    Queue = product_Descriptions_CategoriesVM.product.Queue,
                    LimettedItem= product_Descriptions_CategoriesVM.product.LimettedItem,
                    IsTopSeller= product_Descriptions_CategoriesVM.product.IsTopSeller,

                };
                productRepository.Insert(product);
                p = product;
                //Upload product Images
                if (product_Descriptions_CategoriesVM.formFiles != null)
                {
                    string UniqueFileName = null;
                    string firstImgURL = null;
                    foreach (IFormFile photo in product_Descriptions_CategoriesVM.formFiles)
                    {
                        string UploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "UplodedImages");
                        UniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(UploadsFolder, UniqueFileName);
                        string Url = "~/UplodedImages/" + UniqueFileName;
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        photo.CopyTo(fs);
                        fs.Close(); 
                        ProductImage productImage = new ProductImage()
                        {
                            ProductId = product.Id,
                            ImgUrl = Url,

                        };
                        if (firstImgURL==null)
                        {
                            firstImgURL = Url;
                        }
                     
                        productImageRepository.Insert(productImage);
                 
                    }
                    if (firstImgURL != null)
                    {
                        product.FirstImgUrl = firstImgURL;
                        productRepository.Update(product);

                    }
                }
            }

            return RedirectToAction("CreateProductDescription", new { productID = p.Id });
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {

            Product_Descriptions_CategoriesVM product_Categories = new Product_Descriptions_CategoriesVM()
            {

                product = productRepository.GetById(id),
                Categories = categoryRepository.GetAll(),
                productDescriptions = productDescriptionRepository.GetProductDescriptions(id)
            };
            return View(product_Categories);
          
        }
        [HttpPost]
        public IActionResult Edit(Product_Descriptions_CategoriesVM product_Descriptions_CategoriesVM)
        {
            if (ModelState.IsValid)
            {
                Product OldProduct = productRepository.GetById(product_Descriptions_CategoriesVM.product.Id);
                OldProduct.NameE = product_Descriptions_CategoriesVM.product.NameE;
                OldProduct.NameA = product_Descriptions_CategoriesVM.product.NameA;
                OldProduct.Price = product_Descriptions_CategoriesVM.product.Price;
                OldProduct.Amount = product_Descriptions_CategoriesVM.product.Amount;
                OldProduct.IsActive = product_Descriptions_CategoriesVM.product.IsActive;
                OldProduct.CategoryId = product_Descriptions_CategoriesVM.product.CategoryId;
                OldProduct.HintE = product_Descriptions_CategoriesVM.product.HintE;
                OldProduct.HintA = product_Descriptions_CategoriesVM.product.HintA;
                OldProduct.TagE = product_Descriptions_CategoriesVM.product.TagE;
                OldProduct.TagA = product_Descriptions_CategoriesVM.product.TagA;
                OldProduct.Queue = product_Descriptions_CategoriesVM.product.Queue;
                OldProduct.LimettedItem = product_Descriptions_CategoriesVM.product.LimettedItem;
                OldProduct.IsTopSeller = product_Descriptions_CategoriesVM.product.IsTopSeller;
                productRepository.Update(OldProduct);
                if (product_Descriptions_CategoriesVM.formFiles != null)
                {

                    string firstImgURL = null;

                    List<ProductImage> OldProductImgs = productImageRepository.GetProductImgsByProductID(OldProduct.Id).ToList();
                    if (OldProductImgs.Count > 0)
                    {
                        foreach (ProductImage img in OldProductImgs)
                        {
                            productImageRepository.Delete(img.Id);
                            
                        }
                    }
                    string UniqueFileName = null;
                   
                    foreach (IFormFile photo in product_Descriptions_CategoriesVM.formFiles)
                    {
                        string UploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "UplodedImages");
                        UniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(UploadsFolder, UniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        string Url = "~/UplodedImages/" + UniqueFileName;

                        ProductImage productImage = new ProductImage()
                        {
                            ProductId = OldProduct.Id,
                            ImgUrl = Url,
                        };
                        if (firstImgURL == null)
                        {
                            firstImgURL = Url;
                        }
                        productImageRepository.Insert(productImage);
                    }
                    OldProduct.FirstImgUrl = firstImgURL;
                    productRepository.Update(OldProduct);

                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(long id)
        {
            productRepository.Delete(id);
            return RedirectToAction("Index");
        }
        #region Product Description
        [HttpGet]
        public IActionResult CreateProductDescription(long productID)
        {
            ProductDescription productDescription = new ProductDescription()
            {
                ProductId = productID
            };
            return View(productDescription);
        }
        [HttpPost]
        public IActionResult CreateProductDescription(ProductDescription productDescription)
        {
            productDescriptionRepository.Insert(productDescription);
            return RedirectToAction("CreateProductDescription", new { productID = productDescription.ProductId });
        }
        [HttpGet]
        public IActionResult EditProductDescription(long id)
        {
            ProductDescription productDescription= productDescriptionRepository.GetById(id);
            return View(productDescription);
        }
        [HttpPost]
        public IActionResult EditProductDescription(ProductDescription productDescription)
        {
            productDescriptionRepository.Update(productDescription);
            return RedirectToAction("Edit", new { id = productDescription.ProductId });

        }
        public IActionResult DeleteProductDescription(long id )
        {
            ProductDescription productDescription= productDescriptionRepository.GetById(id);
            productDescriptionRepository.Delete(id);
            return RedirectToAction("Edit",new { id = productDescription.ProductId });


        }
        #endregion
    }
}

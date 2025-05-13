using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BaytonicECommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext context;
        public ProductRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(long ProductID)
        {
            return context.Products.Find(ProductID);
        }
        public void Insert(Product Product)
        {
            context.Products.Add(Product);
            context.SaveChanges();

        }
        public void Update(Product Product)
        {
            context.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(long ProductID)
        {
            Product Product = context.Products.Find(ProductID);
            context.Products.Remove(Product);
            context.SaveChanges();

        }
        //custom Method to get all products with category Id
        public IEnumerable<Product> GetAllByCategory(long id)
        {
            return context.Products.Where(x => x.CategoryId == id).Include(x=>x.Category).OrderBy(x=>x.Queue).AsNoTracking().ToList();
        }
        public IEnumerable<Product> GetProductByTag(string tag)
        {
            return context.Products.Where(x => EF.Functions.Like(x.TagE, $"%{tag}%") || EF.Functions.Like(x.TagA, $"%{tag}%") || EF.Functions.Like(x.NameA, $"%{tag}%") || EF.Functions.Like(x.NameE, $"%{tag}%")).AsNoTracking().ToList();

            //return context.Products.Where(x => x.TagE.Contains(tag) || x.TagA.Contains(tag)).AsNoTracking().ToList();
        }
        public IEnumerable<VwproductWithCategoryName> GetAllProductsByCategoryName()
        {
            return context.VwproductWithCategoryNames.Where(x => x.IsDeleted == false).OrderBy(x => x.Queue).AsNoTracking().ToList();

        }
        //Get offers 
        public IEnumerable<Product> GetProductWithOffer()
        {
            return context.Products.Where(x => x.Offer > 0).AsNoTracking().ToList();
        }
        public IEnumerable<Product> GetTopTenProductWithOffer()
        {
            return context.Products.Where(x => x.Offer > 0).Take(10).AsNoTracking().ToList();
        }
        public IEnumerable<Product> GetTopSellerProducts()
        {
            return context.Products.Where(x => x.IsTopSeller==true).Take(10).AsNoTracking().ToList();
        }
        public IEnumerable<Product> GetTopTenProductsOfCategory(long catId)
        {
            return context.Products.Where(x => x.CategoryId==catId).Take(10).AsNoTracking().ToList();
        }
    }
}


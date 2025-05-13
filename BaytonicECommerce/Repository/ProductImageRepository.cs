using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly EcommerceContext context;
        public ProductImageRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<ProductImage> GetAll()
        {
            return context.ProductImages.ToList();
        }
        public ProductImage GetById(Object ProductImageID)
        {
            return context.ProductImages.Find(ProductImageID);
        }
        public void Insert(ProductImage ProductImage)
        {
            context.ProductImages.Add(ProductImage);
            context.SaveChanges();

        }
        public void Update(ProductImage ProductImage)
        {
            context.Entry(ProductImage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object ProductImageID)
        {
            ProductImage ProductImage = context.ProductImages.Find(ProductImageID);
            context.ProductImages.Remove(ProductImage);
            context.SaveChanges();

        }
        public IEnumerable<ProductImage> GetProductImgsByProductID(long id)
        {
           return context.ProductImages.Where(x => x.ProductId == id).ToList();
        }
    }
}

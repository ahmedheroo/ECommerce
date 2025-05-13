using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class ProductDescriptionRepository : IProductDescriptionRepository
    {
        private readonly EcommerceContext context;
        public ProductDescriptionRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<ProductDescription> GetAll()
        {
            return context.ProductDescriptions.Where(x=>x.IsDeleted==false).ToList();
        }
        public ProductDescription GetById(Object ProductDescriptionID)
        {
            return context.ProductDescriptions.Find(ProductDescriptionID);
        }
        public void Insert(ProductDescription ProductDescription)
        {
            context.ProductDescriptions.Add(ProductDescription);
            context.SaveChanges();

        }
        public void Update(ProductDescription ProductDescription)
        {
            context.Entry(ProductDescription).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object ProductDescriptionID)
        {
            ProductDescription ProductDescription = context.ProductDescriptions.Find(ProductDescriptionID);
            context.ProductDescriptions.Remove(ProductDescription);
            context.SaveChanges();

        }
        public IEnumerable<ProductDescription> GetProductDescriptions(long id) 
        {
            return context.ProductDescriptions.Where(x=>x.IsDeleted==false&&x.ProductId==id).ToList();

        }

    }
}


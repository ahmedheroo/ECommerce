using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceContext context;
        public CategoryRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Category> GetAll()
        {
            return context.Categories.Include(x=>x.Parent).ToList();
        }
       public IEnumerable<Category> GetTopSixCategories()
        {
            return context.Categories.Include(x => x.Parent).Take(6).ToList();

        }
        public IEnumerable<Category> GetShowInHomeCategories()
        {
            return context.Categories.Where(x=>x.ShowInHomePage==true).Include(x => x.Products.OrderBy(y=>y.Queue).Take(10)).ToList();

        }
        public Category GetById(Object CategoryID)
        {
            return context.Categories.Find(CategoryID);
        }
        public void Insert(Category Category)
        {
            context.Categories.Add(Category);
            context.SaveChanges();

        }
        public void Update(Category Category)
        {
            context.Entry(Category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object CategoryID)
        {
            Category Category = context.Categories.Find(CategoryID);
            context.Categories.Remove(Category);
            context.SaveChanges();

        }
        public void DeleteToRecycle(object CatID)
        {
            Category Category = context.Categories.Find(CatID);
            Category.IsDeleted = true;
            Update(Category);

        }
    }
}

using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetTopSixCategories();
        IEnumerable<Category> GetShowInHomeCategories();
        Category GetById(object id);
        void Insert(Category obj);
        void Update(Category obj);
        void Delete(object id);
        void DeleteToRecycle(object CatID);
    }
}

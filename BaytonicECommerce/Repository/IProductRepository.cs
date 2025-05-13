using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(long id);
        void Insert(Product obj);
        void Update(Product obj);
        void Delete(long id);
        //custom Method to get all products with category Id
        IEnumerable<Product> GetAllByCategory(long id);
        IEnumerable<Product> GetProductByTag(string Tag);
        IEnumerable<VwproductWithCategoryName> GetAllProductsByCategoryName();
        IEnumerable<Product> GetProductWithOffer();
        IEnumerable<Product> GetTopTenProductWithOffer();
        IEnumerable<Product> GetTopSellerProducts();
        IEnumerable<Product> GetTopTenProductsOfCategory(long catId);


    }
}

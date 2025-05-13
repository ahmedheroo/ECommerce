using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public  interface IProductImageRepository
    {
        IEnumerable<ProductImage> GetAll();
        ProductImage GetById(object id);
        void Insert(ProductImage obj);
        void Update(ProductImage obj);
        void Delete(object id);
        public IEnumerable<ProductImage> GetProductImgsByProductID(long id);



    }
}

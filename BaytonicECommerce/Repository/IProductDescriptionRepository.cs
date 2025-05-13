using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public interface IProductDescriptionRepository
    {
        IEnumerable<ProductDescription> GetAll();
        ProductDescription GetById(object id);
        void Insert(ProductDescription obj);
        void Update(ProductDescription obj);
        void Delete(object id);
        //id is the productId
      public  IEnumerable<ProductDescription> GetProductDescriptions(long id);


    }
}

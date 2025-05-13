using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface ICartRepository
    {
        IEnumerable<Cart> GetAll() ;
        Cart GetById(object id);
        void Insert(Cart obj);
        void Update(Cart obj);
        void Delete(object id);
        IEnumerable<Cart> GetAllCartUsingUserIdIncludingProducts(string UserId);
        public Cart GetCartUsingUserIdAndProductIdIncludingProduct(string UserId, long productId);
    }
}

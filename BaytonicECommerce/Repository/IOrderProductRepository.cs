using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface IOrderProductRepository
    {
        IEnumerable<OrderProduct> GetAll();
        OrderProduct GetById(object id);
        void Insert(OrderProduct obj);
        void Update(OrderProduct obj);
        void Delete(object id);
        public IEnumerable<OrderProduct> GetOrderProductsByOrderID(long id);
   



    }
}

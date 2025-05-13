using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(long id);
        void Insert(Order obj);
        void Update(Order obj);
        void Delete(object id);
        IEnumerable<Order> GetUserOrders(string UserId);
        Order GetUnpaidUserOrder(string UserId);

    }
}

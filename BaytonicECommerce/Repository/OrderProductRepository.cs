using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public class OrderProductRepository : IOrderProductRepository
    {
        private readonly EcommerceContext context;
        public OrderProductRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<OrderProduct> GetAll()
        {
            return context.OrderProducts.ToList();
        }
        public OrderProduct GetById(Object OrderProductID)
        {
            return context.OrderProducts.Find(OrderProductID);
        }
        public void Insert(OrderProduct OrderProduct)
        {
            context.OrderProducts.Add(OrderProduct);
            context.SaveChanges();

        }
        public void Update(OrderProduct OrderProduct)
        {
            context.Entry(OrderProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object OrderProductID)
        {
            OrderProduct OrderProduct = context.OrderProducts.Find(OrderProductID);
            context.OrderProducts.Remove(OrderProduct);
            context.SaveChanges();

        }
        public IEnumerable<OrderProduct> GetOrderProductsByOrderID(long id)
        {
            return context.OrderProducts.Where(x => x.OrderId == id).Include(x => x.Product).ToList();
        }
    }
}

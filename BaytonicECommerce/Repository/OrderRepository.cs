using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceContext context;
        public OrderRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Order> GetAll()
        {
            return context.Orders.OrderByDescending(x=>x.OrderDate).Include(x=>x.User).ToList();
        }
        public Order GetById(long OrderID)
        {
            return context.Orders.Where(x=>x.Id==OrderID).Include(x=>x.User).Include(x=>x.OrderProducts).ThenInclude(x=>x.Product).Include(x => x.GovShipping).FirstOrDefault();
        }


        public void Insert(Order Order)
        {
            context.Orders.Add(Order);
            context.SaveChanges();

        }
        public void Update(Order Order)
        {
            context.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object OrderID)
        {
            Order Order = context.Orders.Find(OrderID);
            context.Orders.Remove(Order);
            context.SaveChanges();

        }
        public IEnumerable<Order> GetUserOrders(string UserId)
        {
            return context.Orders.Where(x=>x.UserId==UserId).OrderByDescending(x => x.OrderDate).Include(x => x.OrderProducts).Include(x => x.GovShipping).ToList();
        }
        public Order GetUnpaidUserOrder(string UserId)
        {
            return context.Orders.Where(x => x.UserId == UserId && x.Paied == false).FirstOrDefault();
        }


    }
}

using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceContext context;
        public CartRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Cart> GetAll()
        {
            return context.Carts.ToList();
        }
        public Cart GetById(Object CartID)
        {
            return context.Carts.Find(CartID);
        }
        public void Insert(Cart Cart)
        {
            context.Carts.Add(Cart);
            context.SaveChanges();
        }
        public void Update(Cart Cart)
        {
            context.Entry(Cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object CartID)
        {
            Cart Cart = context.Carts.Find(CartID);
            context.Carts.Remove(Cart);
            context.SaveChanges();

        }
        public IEnumerable<Cart> GetAllCartUsingUserIdIncludingProducts(string UserId)
        {
            return context.Carts.Where(x=>x.UserId==UserId).Include(x=>x.Product);
        }
        public Cart GetCartUsingUserIdAndProductIdIncludingProduct(string UserId,long productId)
        {
            return context.Carts.Where(x => x.UserId == UserId&&x.ProductId==productId).Include(x => x.Product).FirstOrDefault();
        }
    }
}

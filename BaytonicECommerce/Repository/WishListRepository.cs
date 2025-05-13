using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly EcommerceContext context;
        public WishListRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable< WishList> GetAll()
        {
            return context. WishLists.ToList();
        }
        public  WishList GetById(Object  WishListID)
        {
            return context. WishLists.Find( WishListID);
        }
        public void Insert( WishList  WishList)
        {
            context. WishLists.Add( WishList);
            context.SaveChanges();

        }
        public void Update( WishList  WishList)
        {
            context.Entry( WishList).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object  WishListID)
        {
             WishList  WishList = context. WishLists.Find( WishListID);
            context. WishLists.Remove( WishList);
            context.SaveChanges();

        }

      public IEnumerable<WishList> GetUserWishListsIncludeProduct(string UserId)
        {
            return context.WishLists.Where(x=>x.UserId==UserId).Include(x=>x.Product).Include(x=>x.Product.Category);

        }
        public WishList GetWishListUsingUserIdAndProductId(string UserId,long productId)
        {
            return context.WishLists.Where(x => x.UserId == UserId&&x.ProductId==productId).FirstOrDefault();

        }

    }
}

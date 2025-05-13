using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public  interface IWishListRepository
    {
        IEnumerable<WishList> GetAll();
        WishList GetById(object id);
        void Insert(WishList obj);
        void Update(WishList obj);
        void Delete(object id);
        IEnumerable<WishList> GetUserWishListsIncludeProduct(string UserId);
        WishList GetWishListUsingUserIdAndProductId(string UserId,long productId);

    }
}

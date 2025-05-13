using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
 public   interface ICouponRepository
    {
        IEnumerable<Coupon> GetAll();
        Coupon GetById(object id);
        Coupon GetByCode(string couponCode);
        bool chkUserCoupon(string userId, long couponId);
        void Insert(Coupon obj);
        void Update(Coupon obj);
        void Delete(object id);
    }
}

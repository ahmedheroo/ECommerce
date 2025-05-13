using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly EcommerceContext context;
        public CouponRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Coupon> GetAll()
        {
            return context.Coupons.ToList();
        }

        public Coupon GetById(Object CouponID)
        {
            return context.Coupons.Find(CouponID);
        }
        public Coupon GetByCode(string couponCode) 
        {
            return context.Coupons.Where(x=> x.Code == couponCode).FirstOrDefault();
        }
        public bool chkUserCoupon(string userId, long couponId)
        {
          long orderId = context.Orders.Where(x => x.DiscountCouponId == couponId && x.UserId ==userId).Select(x=>x.Id).FirstOrDefault();
            return orderId > 0 ;
        }
        public void Insert(Coupon Coupon)
        {
            context.Coupons.Add(Coupon);
            context.SaveChanges();

        }
        public void Update(Coupon Coupon)
        {
            context.Entry(Coupon).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object CouponID)
        {
            Coupon Coupon = context.Coupons.Find(CouponID);
            context.Coupons.Remove(Coupon);
            context.SaveChanges();

        }
    }
}

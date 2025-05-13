using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class Cart_ShipsVM
    {
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<SelectListItem> Governments { set; get; }
        public IEnumerable<Coupon> coupons { set; get; }
    }
}

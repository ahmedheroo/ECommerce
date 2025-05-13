
using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce
{
    public class OrderProductsVM
    {
        public Order order { get; set; }
        public List<OrderProduct> orderProducts { get; set; }
        public AspNetUser client { get; set; }

    }
}

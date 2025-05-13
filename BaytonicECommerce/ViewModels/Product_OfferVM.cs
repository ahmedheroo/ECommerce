using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class Product_OfferVM
    {
        public Product product { get; set; }
        public IEnumerable<Product> Products { get; set; }
       
    }
}

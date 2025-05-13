using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class HomePageVM
    {
        public IEnumerable<Product> ProductsWithOffers { get; set; }
        public IEnumerable<Product> TopSellerProducts { get; set; }
        public IEnumerable<Category> TopCategories { get; set; }
        public IEnumerable<Category> ShowInHomeCategories { get; set; }


    }
}

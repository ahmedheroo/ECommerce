using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<ProductDescription> productDescriptions { get; set; }
        public IEnumerable<Product> relatedProducts { get; set; }

    }
}

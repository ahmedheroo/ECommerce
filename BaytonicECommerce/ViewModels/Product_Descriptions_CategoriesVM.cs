using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class Product_Descriptions_CategoriesVM
    {
        public Product product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductDescription> productDescriptions { get; set; }
        public List<IFormFile> formFiles { get; set; }
    }
}

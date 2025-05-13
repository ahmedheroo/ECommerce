using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class Category_ParentVM
    {
        public Category category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
   
      
    }
}

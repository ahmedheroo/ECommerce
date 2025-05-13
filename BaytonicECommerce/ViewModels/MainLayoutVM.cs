using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.ViewModels
{
    public class MainLayoutVM
    {
        public IEnumerable<Category> AllCategories { get; set; }
        public string PageTitle { get; set; }
    }
}

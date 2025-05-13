using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Components
{
    public class AllCategories:ViewComponent 
    {
        private readonly ICategoryRepository categoryRepository;
        public AllCategories(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categories = categoryRepository.GetAll().OrderBy(x => x.Id);
            return View(categories);
        }
    }
}

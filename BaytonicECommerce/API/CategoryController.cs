
using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.Repository;
namespace BaytonicECommerce.API
{

    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCategories()
        {
            try
            {
                List<Category> Category = categoryRepository.GetAll().ToList();
                return Ok(Category);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetCategory(long id)
        {
            try
            {
                Category Category = categoryRepository.GetById(id);
                return Ok(Category);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

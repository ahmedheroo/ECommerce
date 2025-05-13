
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
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;

        public CartController(ICartRepository _cartRepository)
        {
            cartRepository = _cartRepository;
        }
        [HttpGet]
        [Route("api/[controller]/GetUserCart/{id}")]
        public IActionResult GetUserCart(string id)
        {
            try
            {
                 List<Cart> cartlst = cartRepository.GetAllCartUsingUserIdIncludingProducts(id).ToList();
                return Ok(cartlst);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("api/[controller]/AddUserCartItem")]
        public IActionResult AddUserCartItem([FromBody] Cart newItem)
        {
            Cart cart = new Cart();
            //check if this product added before to cart
            cart = cart = cartRepository.GetCartUsingUserIdAndProductIdIncludingProduct(newItem.UserId, newItem.ProductId);
            
            if (cart == null)
            {
                try
                {
                    cartRepository.Insert(newItem);
                    return Ok(newItem);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost]
        [Route("api/[controller]/UpdateCartItemQuantity")]
        public IActionResult UpdateCartItemQuantity([FromBody] Cart updatedCart)
        {
            try
            {
                Cart cartitem = cartRepository.GetById(updatedCart.Id);
                if (cartitem != null)
                {
                    cartitem.Quantity = updatedCart.Quantity;
                    cartRepository.Update(cartitem);
                }

                return Ok(cartitem);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/[controller]/DeleteCartItem/{id}")]
        public IActionResult DeleteCartItem(long id)
        {
            try
            {
                Cart cartitem = cartRepository.GetById(id);
                if (cartitem != null)
                {
                    cartRepository.Delete(id);
                }

                return Ok(cartitem);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

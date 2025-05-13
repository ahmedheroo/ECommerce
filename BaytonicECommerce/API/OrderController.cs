using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.API
{
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;


        public OrderController(IOrderRepository _orderRepository, IOrderProductRepository _orderProductRepository)
        {
            orderRepository = _orderRepository;
            orderProductRepository = _orderProductRepository;
        }
        [HttpGet]
        [Route("api/[controller]/GetUserOrders/{id}")]
        public IActionResult GetUserOrders(string id)
        {
            List<OrderProductsVM> orderProductsVMs = new List<OrderProductsVM>();
            try
            {

                List<Order> orders = orderRepository.GetUserOrders(id).ToList();
                foreach (var item in orders)
                {
                    OrderProductsVM OrderProductsVM = new OrderProductsVM();
                    OrderProductsVM.order = item;
                    OrderProductsVM.orderProducts = orderProductRepository.GetOrderProductsByOrderID(item.Id).ToList();
                    orderProductsVMs.Add(OrderProductsVM);
                }
                return Ok(orderProductsVMs);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("api/[controller]/GetUserOrder/{id}")]
        public IActionResult GetUserOrder(long id)
        {
            OrderProductsVM orderProductsVM = new OrderProductsVM();
            try
            {
                orderProductsVM.order = orderRepository.GetById(id);
                orderProductsVM.orderProducts = orderProductRepository.GetOrderProductsByOrderID(id).ToList();
                return Ok(orderProductsVM);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/[controller]/AddOrder/{UserID}")]
        public IActionResult AddUserOrder(string UserID, OrderProductsVM orderProductsVM)
        {
            try
            {
                orderRepository.Insert(orderProductsVM.order);
               
                foreach (var item in orderProductsVM.orderProducts)
                {
                    item.OrderId = orderProductsVM.order.Id;
                    orderProductRepository.Insert(item);
                  
                }
                return Ok(orderProductsVM);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[HttpPost]
        //[Route("api/[controller]/AddCart/{UserID}")]
        //public IActionResult AddCart(string UserID, OrderProductsVM orderProductsVM)
        //{
        //    try
        //    {
        //        context.Orders.Add(orderProductsVM.order);
        //        context.SaveChanges();
        //        foreach (var item in orderProductsVM.orderProducts)
        //        {
        //            item.OrderId = orderProductsVM.order.Id;
        //            context.OrderProducts.Add(item);
        //            context.SaveChanges();
        //        }
        //        return Ok(orderProductsVM);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpPost]
        //[Route("api/[controller]/AddProductToOrder/{UserID}")]
        //public IActionResult AddProductToOrder(string UserID, Product product)
        //{
        //    OrderProductsVM orderProductsVM = new OrderProductsVM();
        //    try
        //    {
        //        Order Cart = context.Orders.Where(x => x.UserId == UserID && x.IsCart == true).FirstOrDefault();

        //        if (Cart != null)
        //        {
        //            OrderProduct orderProduct = new OrderProduct()
        //            {
        //                ProductId = product.Id,
        //                OrderId = Cart.Id,
        //                Quantity = 1
        //            };
        //            Cart.OrderProducts.Add(orderProduct);
        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            Order newCart = new Order()
        //            {
        //                UserId=UserID,

        //            };
        //        }
        //    }




        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}


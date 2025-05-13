using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.ViewModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{

    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminOrderController : BaseController
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly ILogger<AdminOrderController> _logger;
        private readonly IAccountManagementRepository accountRepository;

        public AdminOrderController(IOrderRepository _orderRepository,IOrderProductRepository _orderProductRepository,
            ILogger<AdminOrderController> logger, IAccountManagementRepository _accountRepository)
        {
            orderProductRepository = _orderProductRepository;
            orderRepository = _orderRepository;
            _logger = logger;
            accountRepository = _accountRepository;
        }
        public IActionResult Index()
        {
            List<Order> orders = orderRepository.GetAll().ToList();
            return View(orders);
        }
        public IActionResult orderDetails(long id)
        {
            OrderProductsVM orderProducts = new OrderProductsVM();
            orderProducts.order = orderRepository.GetById(id);
            return View(orderProducts);
        }
        [HttpPost]
        public IActionResult UpdateStatues(long id ,string stateus)
        {
            Order order = orderRepository.GetById(id);
            if (order != null)
            {
                order.Status = stateus;
                orderRepository.Update(order);
                NotifyToastr("success", "order Statues changed successfully");
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(long id)
        {
            Order order = orderRepository.GetById(id);
            if (order != null)
            {
                List<OrderProduct> orderProducts = orderProductRepository.GetOrderProductsByOrderID(id).ToList();
                foreach (OrderProduct item in orderProducts)
                {
                    orderProductRepository.Delete(item.Id);
                }
                orderRepository.Delete(id);
            }
            return RedirectToAction("Index");

        }
        public IActionResult DeleteToRecycle(long id)
        {
            Order order = orderRepository.GetById(id);
            if (order != null)
            {
                List<OrderProduct> orderProducts = orderProductRepository.GetOrderProductsByOrderID(id).ToList();
                foreach (OrderProduct item in orderProducts)
                {
                    item.IsDeleted = true;
                    orderProductRepository.Update(item);
                }
                order.IsDeleted = true;
                orderRepository.Update(order);
            }
            return RedirectToAction("Index");

        }
    }
}

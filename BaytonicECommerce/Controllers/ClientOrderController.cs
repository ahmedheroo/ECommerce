using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using BaytonicECommerce.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
    [Authorize]
    public class ClientOrderController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly ICartRepository cartRepository;
        private readonly IGovernmentShippingRepository governmentShippingRepository;
        private readonly ICouponRepository couponRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        IStringLocalizer<Resource> T;

        public ClientOrderController(IProductRepository _productRepository, IOrderRepository _orderRepository, IOrderProductRepository _orderProductRepository,
                                     ICartRepository _cartRepository, IGovernmentShippingRepository _governmentShippingRepository, ICouponRepository _couponRepository,
                                     UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            productRepository = _productRepository;
            orderRepository = _orderRepository;
            orderProductRepository = _orderProductRepository;
            cartRepository = _cartRepository;
            governmentShippingRepository = _governmentShippingRepository;
            couponRepository = _couponRepository;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        [Route("OrderHistory")]
        public IActionResult GetUserOrders()
        {
            string userId = userManager.GetUserId(User);
            IEnumerable<Order> orders = orderRepository.GetUserOrders(userId);
            return View(orders);
        }

        [HttpGet]
        public IActionResult GetOrderDetails(long Id)
        {
            OrderProductsVM orderProducts = new OrderProductsVM();
            orderProducts.order = orderRepository.GetById(Id);
            if (orderProducts.order != null)
            {
                orderProducts.orderProducts = orderProductRepository.GetOrderProductsByOrderID(Id).ToList();
            }
            return View(orderProducts);
        }
        [HttpGet]



        //public IActionResult CreateNewOrder(List<Cart> carts)
        //{
        //    string userId = carts.FirstOrDefault().UserId;
        //    if (userId != null)
        //    {
        //        Order UnpaidUserOrder = orderRepository.GetUnpaidUserOrder(userId);
        //        if (UnpaidUserOrder != null)
        //        {
        //            foreach (var item in carts)
        //            {
        //                OrderProduct orderProduct = new OrderProduct()
        //                {
        //                    OrderId = UnpaidUserOrder.Id,
        //                    ProductId = item.ProductId,
        //                    Quantity = item.Quantity,
        //                    UnitPrice = productRepository.GetById(item.ProductId).Price,
        //                    SubTotal=item.TotalPrice
        //                };

        //                UnpaidUserOrder.OrderProducts.Add(orderProduct);
        //                UnpaidUserOrder.TotalPrice += orderProduct.SubTotal;
        //            }
        //            orderRepository.Update(UnpaidUserOrder);

        //        }
        //        else
        //        {

        //            Order newOrder = new Order()
        //            {
        //                UserId = userId,
        //                OrderDate = DateTime.Now,
        //                Delivered = false,
        //                Paied = false,
        //                PaymentTypeId = 1,
        //                TotalPrice = 0,
        //                DeliveryDate = DateTime.Now.AddDays(3),

        //            };
        //            foreach (var item in carts)
        //            {
        //                OrderProduct orderProduct = new OrderProduct()
        //                {
        //                    ProductId = item.ProductId,
        //                    Quantity = item.Quantity,
        //                    UnitPrice = productRepository.GetById(item.ProductId).Price,
        //                    SubTotal = item.TotalPrice
        //                };

        //                newOrder.OrderProducts.Add(orderProduct);
        //                newOrder.TotalPrice += orderProduct.SubTotal;
        //            }
        //            orderRepository.Insert(newOrder);
        //        }
        //    }
        //    return View();
        //}
        [HttpPost]
        public IActionResult CreateNewOrder(string JsonCart ,long GovId ,string Address, string Coupon ,int Discount)
        {
            string userId = userManager.GetUserId(User);
            Order order = new Order()
            {
                TotalPrice = 0
            };
            GovernmentShipping governmentShipping = governmentShippingRepository.GetById(GovId);
            Coupon coupon = couponRepository.GetByCode(Coupon);
            List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(JsonCart);
            foreach (var item in carts)
            {
                OrderProduct orderProduct = new OrderProduct();
                Product product = productRepository.GetById(item.ProductId);
                orderProduct.ProductId = item.ProductId;
                orderProduct.Quantity = item.Quantity;
                if (product.Offer > 0)
                {
                    orderProduct.UnitPrice = product.Offer;
                    orderProduct.SubTotal = product.Offer * item.Quantity;
                    order.TotalPrice += orderProduct.SubTotal;
                }
                else
                {
                    orderProduct.UnitPrice = product.Price;
                    orderProduct.SubTotal = product.Price * item.Quantity;
                    order.TotalPrice += orderProduct.SubTotal;
                }
                order.OrderProducts.Add(orderProduct);
            }
            decimal subtotal = (decimal)order.TotalPrice;
            order.GovShippingId = GovId;
            order.DetailedAddress = governmentShipping.Name + " , " + Address;
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = order.OrderDate.AddDays(5);
            order.TotalPrice += governmentShipping.ShippingPrice;
            order.UserId = userId;
            order.Status = "OrderProccessing";
            order.Discount = Discount;
            if (coupon!=null && !couponRepository.chkUserCoupon(userId,coupon.Id))
            {
                if (coupon.UpToValue < subtotal)
                {
                    order.DiscountCouponId = coupon.Id;
                    order.DicountValue = (coupon.Percent * subtotal) / 100;
                }
            }
            orderRepository.Insert(order);
            return RedirectToAction("ThankYouPage", "ClientOrder");
        }
        [Route("Purchase")]
        public IActionResult ThankYouPage()
        {
            return View();
        }
        public IActionResult CheckCoupon(string JsonCart,string Coupon, decimal OrderPrice)
        {
            string userId = userManager.GetUserId(User);
            Coupon coupon = couponRepository.GetByCode(Coupon);
            if (coupon != null && couponRepository.chkUserCoupon(userId, coupon.Id))
            {
                return Json(-1);
            }
            else if (coupon != null && !couponRepository.chkUserCoupon(userId, coupon.Id))
            {
                if (coupon.UpToValue > OrderPrice)
                {
                return Json(-5);
                }
                else
                return Json((coupon.Percent*OrderPrice)/100);
            }
            else 
            {
                return Json(0);
            }
        }

        [HttpPost]
        public void UpdateOrderItemQuantity(long orderProductId, int NewQuantity)
        {
            try
            {
                OrderProduct orderProduct = orderProductRepository.GetById(orderProductId);
                decimal oldSubTotal = orderProduct.SubTotal;
                orderProduct.Quantity = NewQuantity;
                orderProduct.SubTotal = NewQuantity * (productRepository.GetById(orderProduct.ProductId).Price);
                orderProductRepository.Update(orderProduct);
                Order order = orderRepository.GetById(orderProduct.OrderId);
                order.TotalPrice -= oldSubTotal;
                order.TotalPrice += orderProduct.SubTotal;
                orderRepository.Update(order);
                // NotifyToastr(T["success"], T["ProductRemovedFromYourCartSuccessfully"]);

            }
            catch
            {
                NotifyToastr(T["error"], T["Can't Update Quantity "]);

            }
        }
        [HttpPost]
        public void DeleteOrderItem(long orderProductId)
        {
            try
            {
                OrderProduct orderProduct = orderProductRepository.GetById(orderProductId);
                orderProductRepository.Delete(orderProductId);
                Order order = orderRepository.GetById(orderProduct.OrderId);
                order.TotalPrice -= orderProduct.SubTotal;
                orderRepository.Update(order);
                NotifyToastr(T["success"], T["ItmeRemovedFromYourOrderSuccessfully"]);

            }
            catch
            {
                NotifyToastr(T["error"], T["Can'tRemoveItemFromYourOrder"]);

            }

        }
        [HttpPost]
        public void DeleteOrder(long orderId)
        {
            try
            {
     
                orderRepository.Delete(orderId);
                NotifyToastr(T["success"], T["YourOrderDeletedSuccessfully"]);

            }
            catch
            {
                NotifyToastr(T["error"], T["Can'DeleteYourOrder"]);

            }

        }
     

    }
}
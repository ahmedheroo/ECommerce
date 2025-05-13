using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public long PaymentTypeId { get; set; }
        public bool Paied { get; set; }
        public long? Discount { get; set; }
        public bool Delivered { get; set; }
        public string UserId { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? DicountValue { get; set; }
        public long? DiscountCouponId { get; set; }
        public bool IsDeleted { get; set; }
        public long? GovShippingId { get; set; }
        public string DetailedAddress { get; set; }
        public string Status { get; set; }

        public virtual GovernmentShipping GovShipping { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

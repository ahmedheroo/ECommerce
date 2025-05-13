using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class GovernmentShipping
    {
        public GovernmentShipping()
        {
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? ShippingPrice { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

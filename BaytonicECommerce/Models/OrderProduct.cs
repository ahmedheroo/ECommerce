using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class OrderProduct
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

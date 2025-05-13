using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Cart
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public string ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Product Product { get; set; }
    }
}

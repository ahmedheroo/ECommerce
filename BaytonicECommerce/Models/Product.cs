using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderProducts = new HashSet<OrderProduct>();
            ProductDescriptions = new HashSet<ProductDescription>();
            WishLists = new HashSet<WishList>();
        }

        public long Id { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsActive { get; set; }
        public int? Amount { get; set; }
        public string HintE { get; set; }
        public string HintA { get; set; }
        public int? Queue { get; set; }
        public decimal Offer { get; set; }
        public string LimettedItem { get; set; }
        public bool IsTopSeller { get; set; }
        public string FirstImgUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string TagE { get; set; }
        public string TagA { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}

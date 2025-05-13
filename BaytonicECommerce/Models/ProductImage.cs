using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class ProductImage
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ImgUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class WishList
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long? ProductId { get; set; }
        public string ImgUrl { get; set; }

        public virtual Product Product { get; set; }
    }
}

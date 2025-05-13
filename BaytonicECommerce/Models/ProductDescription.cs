using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class ProductDescription
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Product Product { get; set; }
    }
}

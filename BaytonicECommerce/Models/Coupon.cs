using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Coupon
    {
        public long Id { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string Code { get; set; }
        public decimal? Percent { get; set; }
        public decimal? UpToValue { get; set; }
        public long? GeneratedById { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

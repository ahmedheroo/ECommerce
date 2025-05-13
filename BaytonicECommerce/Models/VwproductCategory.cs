using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class VwproductCategory
    {
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
        public bool IsDeleted { get; set; }
        public string CatNameE { get; set; }
        public string CatNameA { get; set; }
    }
}

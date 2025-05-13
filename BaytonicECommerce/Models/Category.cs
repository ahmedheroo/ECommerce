using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public bool IsActive { get; set; }
        public long? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public string ImgUrl { get; set; }
        public bool ShowInHomePage { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

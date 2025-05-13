using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class NewsLetter
    {
        public long Id { get; set; }
        public string ClientMail { get; set; }
        public bool IsActive { get; set; }
    }
}

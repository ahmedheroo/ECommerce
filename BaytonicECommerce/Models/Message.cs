using System;
using System.Collections.Generic;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class Message
    {
        public long Id { get; set; }
        public string SenderName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message1 { get; set; }
        public bool Seen { get; set; }
        public string SeenByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}

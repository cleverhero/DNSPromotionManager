using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.Models
{
    public class BagProduct
    {
        public String Id { get; set; }

        public String ProductId { get; set; }
        public Product Product { get; set; }

        public String UserId { get; set; }
        public User User { get; set; }
    }
}

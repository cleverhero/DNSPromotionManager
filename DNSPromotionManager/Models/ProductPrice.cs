using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class ProductPrice
    {

        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        public string BranchId { get; set; }
        public Branch Branch { get; set; }

        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public decimal Value { get; set; }
    }
}

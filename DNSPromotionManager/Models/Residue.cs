using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DNSPromotionManager.Models
{
    public class Residue
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
        [Range(0, 99999)]
        public int Value { get; set; }
    }
}

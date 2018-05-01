using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DNSPromotionManager.Models
{
    public class BranchPromotion
    {
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        public string BranchId { get; set; }
        public Branch Branch { get; set; }

        [Required]
        public string PromotionId { get; set; }
        public Promotion Promotion { get; set; }
    }
}

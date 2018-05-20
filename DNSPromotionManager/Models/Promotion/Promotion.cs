using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DNSPromotionManager.Models
{
    public class Promotion
    {
        [StringLength(36)]
        public string Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Code { get; set; }
        
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Begin { get; set; }
        
        [Required]
        public DateTime End { get; set; }
    }
}

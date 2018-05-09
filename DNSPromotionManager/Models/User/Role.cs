using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DNSPromotionManager.Models
{
    public class Characteristic
    {
        [StringLength(36)]
        public string Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Code { get; set; }
        
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        public List<CharacteristicValue> Variants { get; set; }
    }
}

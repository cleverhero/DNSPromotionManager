using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class ProductCharacteristic
    {
        [StringLength(36)]
        [Key]
        public string Id { get; set; }

        [Required]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public string CharacteristicId { get; set; }
        public Characteristic Characteristic { get; set; }

        [Required]
        public string CharacteristicValueId { get; set; }
        public CharacteristicValue CharacteristicValue { get; set; }
    }
}

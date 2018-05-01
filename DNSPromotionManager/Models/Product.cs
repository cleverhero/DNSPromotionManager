using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class Product
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public string KindId { get; set; }
        public Kind Kind { get; set; }

        public String ParentId { get; set; }
        public Product Parent { get; set; }

        [Required]
        public bool DelFlag { get; set; }

        public IEnumerable<Product> Childrens { get; set; }
    }
}

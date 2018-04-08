using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class Product
    {
        
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        public string KindId { get; set; }
        public Kind Kind { get; set; }

        public string ParentId { get; set; }
        public Product Parent { get; set; }

        [Required]
        public bool DelFlag { get; set; }

        public static Dictionary<String, String> GetColumnNames()
        {
            Dictionary<String, String> items = new Dictionary<string, string>();

            items.Add("Code",     "Код");
            items.Add("Name",     "Название");
            items.Add("Kind",     "Вид");
            items.Add("Parent",   "Родитель");
            items.Add("DelFlag",  "Товар удален");

            return items;
        }
    }
}

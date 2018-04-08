using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class Branch
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

        public static Dictionary<String, String> GetColumnNames()
        {
            Dictionary<String, String> items = new Dictionary<string, string>();

            items.Add("Code", "Код");
            items.Add("Name", "Название");

            return items;
        }
    }
}

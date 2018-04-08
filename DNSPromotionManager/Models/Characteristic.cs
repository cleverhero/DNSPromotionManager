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
        [Display(Name="Название")]
        [Required]
        public string Name { get; set; }

        public static Dictionary<String, String> GetColumnNames()
        {
            Dictionary<String, String> items = new Dictionary<string, string>();

            items.Add("Id",   "ID");
            items.Add("Code", "Код");
            items.Add("Name", "Название");

            return items;
        }
    }
}

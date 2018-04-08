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
        [Display(Name="Название")]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Begin { get; set; }
        
        [Required]
        public DateTime End { get; set; }

        public static Dictionary<String, String> GetColumnNames()
        {
            Dictionary<String, String> items = new Dictionary<string, string>();

            items.Add("Id",    "ID");
            items.Add("Code",  "Код");
            items.Add("Name",  "Название");
            items.Add("Begin", "Начало");
            items.Add("End",   "Конец");

            return items;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public class User
    {
        [Key]
        public String Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required]
        public String BranchId { get; set; }
        public Branch Branch { get; set; }

        [Required]
        public String RoleId { get; set; }
        public Role Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DNSPromotionManager.Models
{
    public enum UserRole
    {
        User, Admin
    }

    public class User
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public String BranchId { get; set; }
        public Branch Branch { get; set; }

        public UserRole Role { get; set; }
    }
}

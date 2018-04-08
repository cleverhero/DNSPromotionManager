using System;
using System.Collections.Generic;
using System.Linq;
using DNSPromotionManager.Models;

namespace DNSPromotionManager
{
    public static class SampleData
    {
        public static void Initialize(DNSContext context)
        {
            if (!context.Kinds.Any())
            {
                context.Kinds.AddRange(
                    new Kind
                    {
                        Name = "iPhone 6S",
                        Code = "Apple"
                    },
                    new Kind
                    {
                        Name = "Samsung Galaxy Edge",
                        Code = "Samsung"
                    },
                    new Kind
                    {
                        Name = "Lumia 950",
                        Code = "Microsoft"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

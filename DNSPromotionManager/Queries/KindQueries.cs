using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNSPromotionManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DNSPromotionManager.Queries
{
    public class KindQueries
    {
        static public List<Kind> Kinds(DNSContext db)
        {
            return db.Kinds.ToList();
        }

        static public List<Kind> Kind(DNSContext db, String KindId)
        {
            return db.Kinds
                .Where(item => item.Id == KindId)
                .ToList();
        }
    }
}

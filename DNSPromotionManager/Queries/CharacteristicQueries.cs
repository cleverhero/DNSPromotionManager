using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNSPromotionManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DNSPromotionManager.Queries
{
    public class CharacteristicQueries
    {
        static public List<Characteristic> Characteristics(DNSContext db)
        {
            return db.Characteristics.Include(i => i.Variants).ToList();
        }

        static public List<CharacteristicValue> CharacteristicValues(DNSContext db, String CharacteristicId)
        {
            return db.CharacteristicValues
                .Where(item => item.CharacteristicId == CharacteristicId)
                .ToList();
        }
    }
}

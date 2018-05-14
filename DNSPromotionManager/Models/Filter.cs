using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.Models
{
    public class CharacteristicFilter
    {
        public String Id;
        public List<String> Variants;
    }
    public class Filter
    {
        public String Name;
        public String Code;
        public Decimal MinPrice;
        public Decimal MaxPrice;

        public List<CharacteristicFilter> Characteristics;

        public Filter()
        {
            Name = "";
            Code = "";
            Characteristics = new List<CharacteristicFilter>();
        }
    }
}

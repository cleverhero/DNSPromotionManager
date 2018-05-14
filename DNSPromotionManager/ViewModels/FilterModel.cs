using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public class FilterModel
    {
        public String Name;
        public String Code;

        public decimal MinPrice;
        public decimal MaxPrice;
        public List<CharacteristicModel> Characteristics;

        public FilterModel()
        {
            Name = "";
            Code = "";

            Characteristics = new List<CharacteristicModel>();
        }
    }
}

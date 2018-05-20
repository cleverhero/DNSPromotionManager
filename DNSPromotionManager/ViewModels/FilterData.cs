using System;
using System.Collections.Generic;
using DNSPromotionManager.ViewModels;
using DNSPromotionManager.Models;

namespace DNSPromotionManager.ViewModels
{
    public class FilterData
    {
        public String Name;
        public String Code;
        public Decimal MinPrice;
        public Decimal MaxPrice;

        public List<Kind> Kinds;
        public List<Characteristic> Characteristics;

        public FilterData()
        {
            Name = "";
            Code = "";

            Kinds = new List<Kind>();
            Characteristics = new List<Characteristic>();
        }
    }
}

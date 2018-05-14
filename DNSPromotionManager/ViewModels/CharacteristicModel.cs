using System;
using System.Collections.Generic;
using DNSPromotionManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public struct SelectedVariant
    {
        public String Id;
        public String Name;
        public bool IsSelected;

        public SelectedVariant(String id, String name)
        {
            Id = id;
            Name = name;
            IsSelected = false;
        }
    }

    public class CharacteristicModel
    {
        public string Id;
        public string Caption;

        public List<SelectedVariant> Variants;

        public CharacteristicModel(Characteristic item, List<SelectedVariant> variants)
        {
            Id = item.Id;
            Caption = item.Name;
            Variants = variants;
        }
    }
}

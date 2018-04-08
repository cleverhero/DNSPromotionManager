using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager
{
    public enum TableItemEvent
    {
        Delete,
        Edit,
        Create,
    }

    static public class Global
    {
        static public List<String> Tables = new List<string>
        {
            "Kinds",
            "Products",
            "Branchs",
            "Promotions",
            "Characteristics",
            "CharacteristicValues",
        };

        static public Dictionary<String, String> TableNames = new Dictionary<String, String>
        {
            { "Kinds",                 "Виды товаров" },
            { "Products",              "Товары" },
            { "Branchs",               "Филиалы" },
            { "Promotions",            "Акции" },
            { "Characteristics",       "Характеристики" },
            { "CharacteristicValues",  "Значения характеристик" },
        };
    }
}

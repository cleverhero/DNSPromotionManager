﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public class CardViewModel
    {
        public KeyValuePair<String, String> TableName;
        public Dictionary<String, String> ColumnNames;
        public Dictionary<String, String> Item;
    }
}

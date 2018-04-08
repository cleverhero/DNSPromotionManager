using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public class TableViewModel
    {
        public KeyValuePair<String, String> TableName;
        public Dictionary<String, String> ColumnNames;
        public List<Dictionary<String, String>> Table;
    }
}

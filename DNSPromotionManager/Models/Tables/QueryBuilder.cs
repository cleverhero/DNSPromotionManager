using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.Models
{
    public class QueryBuilder
    {
        public List<String> Joins;
        public List<String> Selects;
        public String TableName;

        public QueryBuilder(String tableName)
        {
            TableName = tableName;
            Joins = new List<string>();
            Selects = new List<string>();
        }

        public static String NextAlias()
        {
            Random random = new Random();
            int val = random.Next(10000, 99999);
            return "t" + val.ToString();
        }

        public String Build()
        {
            return "Select " + String.Join(", ", Selects) + " From " + TableName + "\n" +
                String.Join("\n", Joins);
        }
    }
}

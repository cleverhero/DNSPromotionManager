using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNSPromotionManager.Models;

namespace DNSPromotionManager.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<String> RequestResult;
        public Dictionary<String, String> Tables;
    }
}

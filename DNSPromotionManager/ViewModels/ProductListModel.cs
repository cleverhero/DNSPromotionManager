using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public class ProductListModel
    {
        public List<FatProduct> FatProducts;

        public ProductListModel()
        {
            FatProducts = new List<FatProduct>();
        }
    }
}

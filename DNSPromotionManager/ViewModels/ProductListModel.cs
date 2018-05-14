using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.ViewModels
{
    public class ProductListModel
    {
        public FilterModel Filter;
        public List<FatProduct> FatProducts;

        public ProductListModel()
        {
            Filter = new FilterModel();
            FatProducts = new List<FatProduct>();
        }
    }
}

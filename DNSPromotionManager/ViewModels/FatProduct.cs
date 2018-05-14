using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNSPromotionManager.Models;

namespace DNSPromotionManager.ViewModels
{
    public class FatProduct
    {
        public Product Product;
        public decimal Price;
        public int Residue;

        public List<ProductCharacteristic> Characteristics;
    }
}

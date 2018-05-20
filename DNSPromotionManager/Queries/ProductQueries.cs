using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNSPromotionManager.Models;
using DNSPromotionManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DNSPromotionManager.Queries
{
    public class ProductQueries
    {
        static public FatProduct FatProducts(DNSContext db, String ProductId)
        {
            return (from item in FatProductsQuery(db)
                    where item.Product.Id == ProductId
                    select item).FirstOrDefault();
        }

        static public List<FatProduct> FatProducts(DNSContext db)
        {
            return FatProductsQuery(db).ToList();
        }

        static public List<FatProduct> FatProducts(DNSContext db, List<String> ProductIds)
        {
            var query = FatProductsQuery(db);
            query = query.Where(item => ProductIds.Contains(item.Product.Id));

            return query.ToList();
        }

        static public List<FatProduct> FatProducts(DNSContext db, Filter filter)
        {
            var query = FatProductsQuery(db);
            if (filter == null) return query.ToList();

            if (filter.Name != "") query = query.Where(item => item.Product.Name == filter.Name);
            if (filter.Code != "") query = query.Where(item => item.Product.Code == filter.Code);
            query = query.Where(item => item.Price >= filter.MinPrice);
            query = query.Where(item => item.Price <= filter.MaxPrice);

            if (filter.Kinds.Count > 0)
                query = query.Where(item => filter.Kinds.Contains(item.Product.Kind.Id));

            foreach (var characteristic in filter.Characteristics) {
                query = query.Where(item => 
                    item.Characteristics
                        .Select(c => characteristic.Variants.Contains(c.CharacteristicValueId))
                        .Contains(true)
                );
            }

            return query.ToList();
        }

        static private IQueryable<FatProduct> FatProductsQuery(DNSContext db)
        {
            return (from p in db.Products
                    join kind in db.Kinds on p.KindId equals kind.Id
                    join r in db.Residues on p.Id equals r.ProductId
                    join price in db.ProductPrices on p.Id equals price.ProductId
                    join c in db.ProductCharacteristics on p.Id equals c.ProductId
                    where price.Branch.Name == "Владивосток" &&
                          r.Branch.Name == "Владивосток"
                    group c by new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Code = p.Code,
                        Parent = p.Parent,
                        Kind = kind,
                        Residue = r.Value,
                        Price = price.Value,
                    } into g
                    select new FatProduct
                    {
                        Product = new Product
                        {
                            Id = g.Key.Id,
                            Name = g.Key.Name,
                            Code = g.Key.Code,
                            Parent = g.Key.Parent,
                            Kind = g.Key.Kind
                        },
                        Residue = g.Key.Residue,
                        Price = g.Key.Price,
                        Characteristics = g.ToList()
                    });          
        }


    }
}

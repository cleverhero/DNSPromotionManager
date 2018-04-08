using System.Diagnostics;
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DNSPromotionManager.Models
{
    public class DNSContext : DbContext
    {
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }

        public DNSContext(DbContextOptions<DNSContext> options)
            : base(options)
        {
        }

        delegate String GetName(string id);

//------------------------------------------------------------------------------
//-------------------------------Products---------------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetProducts()
        {
            GetName productString = (id) =>
            {
                var prod = GetProduct(id);
                return prod == null ? null : "Код: " + prod.Code + "\nНазвание: " + prod.Name;
            };

            GetName kindString = (id) =>
            {
                var kind = GetKind(id);
                return kind == null ? null : "Код: " + kind.Code + "\nНазвание: " + kind.Name;
            };

            return Products.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"] = item.Id,
                ["Name"] = item.Name,
                ["Code"] = item.Code,
                ["ParentId"] = item.ParentId,
                ["KindId"] = item.KindId,
                ["Parent"] = productString(item.ParentId),
                ["Kind"] = kindString(item.KindId),
                ["DelFlag"] = item.DelFlag.ToString(),
            }).ToList();
        }

        public Product GetProduct(string id)
        {
            var products = Products.Where(p => p.Id == id).ToList();
            return products.Count == 0 ? null : products[0];
        }

//------------------------------------------------------------------------------
//-------------------------------Kinds------------------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetKinds()
        {
            return Kinds.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"] = item.Id,
                ["Name"] = item.Name,
                ["Code"] = item.Code,
            }).ToList();
        }
        public Kind GetKind(string id)
        {
            var kinds = Kinds.Where(p => p.Id == id).ToList();
            return kinds.Count == 0 ? null : kinds[0];
        }

//------------------------------------------------------------------------------
//-------------------------------Branchs------------------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetBranchs()
        {
            return Branchs.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"] = item.Id,
                ["Name"] = item.Name,
                ["Code"] = item.Code,
            }).ToList();
        }
        public Branch GetBranch(string id)
        {
            var brachs = Branchs.Where(p => p.Id == id).ToList();
            return brachs.Count == 0 ? null : brachs[0];
        }


//------------------------------------------------------------------------------
//-------------------------------Promotions-------------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetPromotions()
        {
            return Promotions.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"]    = item.Id,
                ["Name"]  = item.Name,
                ["Code"]  = item.Code,
                ["Begin"] = item.Begin.ToString(),
                ["End"]   = item.End.ToString(),
            }).ToList();
        }
        public Promotion GetPromotion(string id)
        {
            var promotions = Promotions.Where(p => p.Id == id).ToList();
            return promotions.Count == 0 ? null : promotions[0];
        }

//------------------------------------------------------------------------------
//---------------------------Characteristic-------------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetCharacteristics()
        {
            return Characteristics.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"] = item.Id,
                ["Name"] = item.Name,
                ["Code"] = item.Code,
            }).ToList();
        }
        public Characteristic GetCharacteristic(string id)
        {
            var characteristics = Characteristics.Where(p => p.Id == id).ToList();
            return characteristics.Count == 0 ? null : characteristics[0];
        }

//------------------------------------------------------------------------------
//---------------------------CharacteristicValue--------------------------------
//------------------------------------------------------------------------------
        public List<Dictionary<String, String>> GetCharacteristicValues()
        {
            GetName characteristicString = (id) =>
            {
                var kind = GetCharacteristic(id);
                return kind == null ? null : "Код: " + kind.Code + "\nНазвание: " + kind.Name;
            };

            return CharacteristicValues.ToList().Select(item => new Dictionary<String, String>
            {
                ["Id"] = item.Id,
                ["Name"] = item.Name,
                ["Code"] = item.Code,
                ["Characteristic"] = characteristicString(item.CharacteristicId),
            }).ToList();
        }
        public CharacteristicValue GetCharacteristicValue(string id)
        {
            var list = CharacteristicValues.Where(p => p.Id == id).ToList();
            return list.Count == 0 ? null : list[0];
        }


    }
}


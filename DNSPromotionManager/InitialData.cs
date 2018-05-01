using System;
using System.Collections.Generic;
using System.Linq;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager
{
    public static class SampleData
    {
        public static Dictionary<String, List<String>> Characteristics = new Dictionary<String, List<String>>()
        {
            ["Цвет"] = new List<string>() { "Красный", "Синий", "Зеленый" },
            ["Размер"] = new List<string>() { "Большой", "Малый", "Средний" },
            ["Вес"] = new List<string>() { "Большой", "Малый", "Средний" },
            ["Производитель"] = new List<string>() { "Asus", "Aser", "Dell", "Apple", "Sumsung" },
        };

        public static List<String> Kinds = new List<String>()
        {
            "Смартфоны", "Ноутбуки", "Настольные ПК", "Телевизоры", "Приставки"
        };

        public static List<String> Branchs = new List<String>()
        {
            "Владивосток", "Москва", "Санкт-Петербург", "Екатеринбург", "Сочи"
        };

        public static void InitKinds(DNSContext context)
        {
            if (!context.Kinds.Any())
            {
                var i = 0;
                foreach (var kind in Kinds)
                {
                    i++;
                    context.Kinds.Add(new Kind()
                    {
                        Name = kind,
                        Code = i.ToString()
                    });
                }

                context.SaveChanges();
            }
        }

        public static void InitBranchs(DNSContext context)
        {
            if (!context.Branchs.Any())
            {
                var i = 0;
                foreach (var branch in Branchs)
                {
                    i++;
                    context.Branchs.Add(new Branch()
                    {
                        Name = branch,
                        Code = i.ToString()
                    });
                }

                context.SaveChanges();
            }
        }

        public static void InitProducts(DNSContext context)
        {
            if (!context.Products.Any())
            {
                var rand = new Random();
                var i = 0;
                foreach (var kind in Kinds)
                {
                    var kindid = context.Kinds.Where(k => k.Name == kind)
                        .ToList()[0].Id;
                    foreach (int k in Enumerable.Range(0, rand.Next(1, 50)))
                    {
                        i++;
                        var products = context.Products.ToList();
                        var parentid = products.Count == 0 ?
                            null : products[rand.Next(0, products.Count)].Id;
                        context.Products.Add(new Product()
                        {
                            Name = "Product_" + rand.Next(10000000, 99999999).ToString(),
                            Code = i.ToString(),
                            DelFlag = rand.Next(0, 2) == 0,
                            KindId = kindid,
                            ParentId = parentid
                        });
                        context.SaveChanges();
                    }
                }

                context.SaveChanges();
            }
        }

        public static void InitCharacteristics(DNSContext context)
        {
            if (!context.Characteristics.Any())
            {
                var i = 0;
                foreach (var item in Characteristics)
                {
                    i++;
                    context.Characteristics.Add(new Characteristic()
                    {
                        Name = item.Key,
                        Code = i.ToString()
                    });
                    context.SaveChanges();
                    var j = 0;
                    var lastid = context.Characteristics.Where(charct => charct.Code == i.ToString())
                        .ToList()[0].Id;
                    foreach (var val in item.Value)
                    {
                        j++;
                        context.CharacteristicValues.Add(new CharacteristicValue()
                        {
                            Name = val,
                            CharacteristicId = lastid,
                            Code = j.ToString()
                        });
                    }
                }

                context.SaveChanges();
            }
        }

        public static void InitResidues(DNSContext context)
        {
            if (!context.Residues.Any())
            {
                var rand = new Random();
                var i = 0;
                var items = context.Products;

                foreach (var item in items)
                {
                    var branchs = context.Branchs;
                    foreach (var branch in branchs)
                    {
                        i++;
                        context.Residues.Add(new Residue()
                        {
                            BranchId = branch.Id,
                            ProductId = item.Id,
                            Value = rand.Next(0, 199)
                        });
                    }      
                }

                context.SaveChanges();
            }
        }

        public static void InitProductPrices(DNSContext context)
        {
            if (!context.ProductPrices.Any())
            {
                var rand = new Random();
                var i = 0;
                var items = context.Products;

                foreach (var item in items)
                {
                    var branchs = context.Branchs;
                    foreach (var branch in branchs)
                    {
                        i++;
                        context.ProductPrices.Add(new ProductPrice()
                        {
                            BranchId = branch.Id,
                            ProductId = item.Id,
                            Value = rand.Next(500, 20000)
                        });
                    }
                }

                context.SaveChanges();
            }
        }

        public static void InitProductCharacteristics(DNSContext context)
        {
            if (!context.ProductCharacteristics.Any())
            {
                var rand = new Random();
                var i = 0;
                var items = context.Products;

                foreach (var item in items)
                {
                    var characteristics = context.Characteristics;
                    foreach (var characteristic in characteristics)
                    {
                        var values = context.CharacteristicValues.
                            Where(c => c.CharacteristicId == characteristic.Id).ToList();

                        i++;
                        if (characteristic == null)
                            Console.Write(true);

                        if (item == null)
                            Console.Write(true);

                        var val = values[rand.Next(0, values.Count)];

                        if (val == null)
                            Console.Write(true);

                        context.ProductCharacteristics.Add(new ProductCharacteristic()
                        {
                            CharacteristicId = characteristic.Id,
                            ProductId = item.Id,
                            CharacteristicValueId = val.Id
                        });
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.Write(true);
                            return;
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public static void Initialize(DNSContext context)
        {
            InitKinds(context);
            InitBranchs(context);
            InitProducts(context);
            InitCharacteristics(context);
            InitResidues(context);
            InitProductPrices(context);
            InitProductCharacteristics(context);
        }
    }
}

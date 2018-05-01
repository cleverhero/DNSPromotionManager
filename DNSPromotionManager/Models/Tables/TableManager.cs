using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.Models
{
    public class TableManager
    {
        private static TableManager instance;
        public Dictionary<String, Table> Tables;
        public String ConnectionString;

        public static TableManager getInstance()
        {
            if (instance == null)
                instance = new TableManager(null);
            return instance;
        }

        public static TableManager getInstance(String connection)
        {
            if (instance == null)
                instance = new TableManager(connection);
            return instance;
        }

        private TableManager(String connection)
        {
            ConnectionString = connection;
            Tables = new Dictionary<string, Table>();

            RegisterTable("Products", "Товары").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название").
                AddColumn("KindId", "Вид", "Kinds", "Id", "Kind", new String[] { "Name" }).
                AddColumn("ParentId", "Родитель", "Products", "Id", "Parent", new String[] { "Name" }).
                AddColumn("DelFlag", "Товар удален");

            RegisterTable("Kinds", "Вид").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название");

            RegisterTable("Promotions", "Акции").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название").
                AddColumn("Begin", "Начало").
                AddColumn("End", "Конец");

            RegisterTable("Branchs", "Филлиалы").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название");

            RegisterTable("Characteristics", "Характеристики").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название");

            RegisterTable("CharacteristicValues", "Значения характеристик").
                AddColumn("Id", "Id", false).
                AddColumn("Code", "Код").
                AddColumn("Name", "Название").
                AddColumn("CharacteristicId", "Характеристика", "Characteristics", "Id", "Characteristic", new String[] { "Name" });

            RegisterTable("BranchPromotions", "Акции филлиала").
                AddColumn("Id", "Id", false).
                AddColumn("BranchId", "Филлиал", "Branchs", "Id", "Branch", new String[] { "Name" }).
                AddColumn("PromotionId", "Акция", "Promotions", "Id", "Promotion", new String[] { "Name" });

            RegisterTable("Residues", "Остаток товара").
                AddColumn("Id", "Id", false).
                AddColumn("BranchId", "Филлиал", "Branchs", "Id", "Branch", new String[] { "Name" }).
                AddColumn("ProductId", "Товар", "Products", "Id", "Product", new String[] { "Name" }).
                AddColumn("Value", "Остаток");

            RegisterTable("ProductPrices", "Цена товара").
                AddColumn("Id", "Id", false).
                AddColumn("BranchId", "Филлиал", "Branchs", "Id", "Branch", new String[] { "Name" }).
                AddColumn("ProductId", "Товар", "Products", "Id", "Product", new String[] { "Name" }).
                AddColumn("Value", "Цена");

            RegisterTable("ProductCharacteristics", "Характеристики товара").
                AddColumn("Id", "Id", false).
                AddColumn("ProductId", "Товар", "Products", "Id", "Product", new String[] { "Name" }).
                AddColumn("CharacteristicId", "Характеристика", "Characteristics", "Id", "Characteristic", new String[] { "Name" }).
                AddColumn("CharacteristicValueId", "Значение", "CharacteristicValues", "Id", "CharacteristicValue", new String[] { "Name" });
        }

        private Table RegisterTable(String name, String caption)
        {
            Table newTable = new Table(name, caption);
            Tables.Add(name, newTable);
            return newTable;
        }
    }
}

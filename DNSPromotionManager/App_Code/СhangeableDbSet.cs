using DNSPromotionManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNSPromotionManager.App_Code
{
    static public class СhangeableDbSet
    {
        static public void Change<T>(this DbSet<T> table, T model, TableItemEvent e)
            where T: class
        {
            switch (e)
            {
                case TableItemEvent.Create:
                    table.Add(model);
                    break;
                case TableItemEvent.Edit:
                    table.Update(model);
                    break;
                case TableItemEvent.Delete:
                    table.Remove(model);
                    break;
            }
        }

        static public void Clear<T>(this DbSet<T> table)
            where T : class
        {
            var items = table.ToList();
            foreach (var item in items) table.Remove(item);
        }
    }
}

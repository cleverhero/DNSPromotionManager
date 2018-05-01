using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNSPromotionManager.Models
{
    public abstract class Column
    {
        public String Name;
        public String Caption;
        public String Alias;
        public bool IsVisible;
        public Column(String name, String caption, String alias, bool isVisible)
        {
            Name      = name;
            Caption   = caption;
            Alias     = alias;
            IsVisible = isVisible;
        }

        public abstract void Select(QueryBuilder builder);
    }

    class Link : Column
    {
        public String TableName;
        public String ColumnName;
        public String AsCol;
        public String[] VisibleCols;

        public Link(String name,
                    String caption,
                    String ptable,
                    String pcol,
                    String ascol,
                    String[] visiblecols,
                    bool isVisible = true):
            base(name, caption, ascol, isVisible)
        {
            TableName = ptable;
            ColumnName = pcol;
            AsCol = ascol;
            VisibleCols = visiblecols;
        }

        public override void Select(QueryBuilder builder)
        {
            String alias = QueryBuilder.NextAlias();
            String val = String.Join(" + ", VisibleCols.Select(item => alias + ".[" + item + "]"));
            builder.Selects.Add(val + " as " + AsCol);

            String join = "left join " + TableName + " as " + alias + " on " +
                builder.TableName + "." + Name + " = " +
                alias + "." + ColumnName;
            builder.Joins.Add(join);
        }
    }

    public class SimpleColumn : Column
    {
        public SimpleColumn(String name, String caption, bool isVisible = true): 
            base(name, caption, name, isVisible)
        {
            Name = name;
            Caption = caption;
        }

        public override void Select(QueryBuilder builder)
        {
            builder.Selects.Add(builder.TableName + ".[" + Name + "]");
        }
    }
}
